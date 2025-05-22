# Kubernetes

Kustomer can be run several different ways such as IIS and Docker, however, the main containerisation we are going to utilize is Kubernetes.

Several files in the repository are responsible for configuration and deployment of the application:
```
kustomer-dashboard-deployment.yaml
kustomer-deployment.yaml
kustomer-service.yaml
kustomer.dashboard-service.yaml
Kustomer.API\Dockerfile
```

Locally, we are developing with Minikube, and you can find the installation instructions [HERE](https://minikube.sigs.k8s.io/docs/start/?arch=%2Fwindows%2Fx86-64%2Fstable%2F.exe+download).

To run Kustomer once Minikube or an alternative is installed:
```
kubectl apply -f kustomer-service.yaml,kustomer.dashboard-service.yaml,kustomer-deployment.yaml,kustomer-dashboard-deployment.yaml
```
and you will see something like the following output:
```
service/kustomer created
service/kustomer-dashboard created
deployment.apps/kustomer created
deployment.apps/kustomer-dashboard created
```

To access your application you can use the following if you are using Minikube:
```
minikube service kustomer

# or describe
kubectl describe svc kustomer
```