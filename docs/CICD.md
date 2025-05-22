# CICD

![CICD](/docs/assets/cicd_pipeline.png)

The CI/CD pipeline for this microservice on Kubernetes utilizes Github as the repository and integrates with Azure DevOps to run the pipeline. Azure DevOps pipeline then can run automated testing, build the applications, build the container, store the container in Azure Container Registry, and deploy to a service like Azure Kubernetes Service (AKS) or Azure Container Apps. The pipeline has configurations to run one or many of these actions based on the trigger.

### Integration
DevOps project is created under your organization, a new pipeline is selected and the github repository is chosen as the target.

### Triggers
CI Triggers are configured and look something like below, which would trigger anytime `main`, or any branch that prefixes with `releases/` or `feature/`
```
trigger:
  branches:
    include:
    - main
    - releases/*
    - feature/*
```
### Pipeline

You can build scripts into the pipeline files for things like testing, here is an example:
```
steps:
# ...
- script: dotnet test $(testProject)
```

Example build stage:
```
- stage: Build
  displayName: Build stage
  jobs:  
  - job: Build
    displayName: Build job
    pool:
      vmImage: $(vmImageName)
    steps:
    - task: Docker@2
      displayName: Build and push an image to container registry
      inputs:
        command: buildAndPush
        repository: $(imageRepository)
        dockerfile: $(dockerfilePath)
        containerRegistry: $(dockerRegistryServiceConnection)
        tags: |
          $(tag)
          
    - task: PublishPipelineArtifact@1
      inputs:
        artifactName: 'manifests'
        path: 'manifests'
```

Example Deploy stage:
```
- stage: Deploy
  displayName: Deploy stage
  dependsOn: Build
  jobs:
  - deployment: Deploy
    displayName: Deploy job
    pool:
      vmImage: $(vmImageName)
    environment: 'myenv.aksnamespace' #customize with your environment
    strategy:
      runOnce:
        deploy:
          steps:
          - task: DownloadPipelineArtifact@2
            inputs:
              artifactName: 'manifests'
              downloadPath: '$(System.ArtifactsDirectory)/manifests'

          - task: KubernetesManifest@1
            displayName: Create imagePullSecret
            inputs:
              action: 'createSecret'
              connectionType: 'kubernetesServiceConnection'
              kubernetesServiceConnection: 'myapp-default' #customize for your Kubernetes service connection
              secretType: 'dockerRegistry'
              secretName: '$(imagePullSecret)'
              dockerRegistryEndpoint: '$(dockerRegistryServiceConnection)'

          - task: KubernetesManifest@1
            displayName: Deploy to Kubernetes cluster
            inputs:
              action: 'deploy'
              connectionType: 'kubernetesServiceConnection'
              kubernetesServiceConnection: 'myapp-default' #customize for your Kubernetes service connection
              manifests: |
                $(Pipeline.Workspace)/manifests/deployment.yml
                $(Pipeline.Workspace)/manifests/service.yml
              containers: '$(containerRegistry)/$(imageRepository):$(tag)'
              imagePullSecrets: '$(imagePullSecret)'
```
