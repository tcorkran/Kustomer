import requests
import json

# Define the API endpoint
url = "http://localhost:8080/Customer"

# Example GET request
def get_customers():
    response = requests.get(url)
    if response.status_code == 200:
        customers = response.json()
        print("GET request successful:")
        print(json.dumps(customers, indent=2))
    else:
        print(f"GET request failed with status code: {response.status_code}")

# Example POST request
def create_customer():
    new_customer = {
        "firstName": "Patrick",
        "middleName": "Ryan",
        "lastName": "Python",
        "email": "patpython@test.com",
        "phone": "555-999-6789"
    }
    response = requests.post(url, json=new_customer)
    if response.status_code == 200:
        created_customer = response.json()
        print("POST request successful:")
        print(json.dumps(created_customer, indent=2))
        return created_customer.get("id")
    else:
        print(f"POST request failed with status code: {response.status_code}")

# Example GET Details request
def get_customer(customer_id):
    response = requests.get(f"{url}/{customer_id}")
    if response.status_code == 200:
        customer_details = response.json()
        print("GET Details request successful:")
        print(json.dumps(customer_details, indent=2))
    else:
        print(f"GET Details request failed with status code: {response.status_code}")

# Example PUT request
def update_customer(customer_id):
    updated_customer = {
        "firstName": "Patrick",
        "middleName": "Uses",
        "lastName": "Python",
        "email": "usepython@test.com",
        "phone": "555-999-6789"
    }
    response = requests.put(f"{url}/{customer_id}", json=updated_customer)
    if response.status_code == 200:
        updated_customer_details = response.json()
        print("PUT request successful:")
        print(json.dumps(updated_customer_details, indent=2))
    else:
        print(f"PUT request failed with status code: {response.status_code}")

# Example DELETE request
def delete_customer(customer_id):
    response = requests.delete(f"{url}/{customer_id}")
    if response.status_code == 200:
        print(f"DELETE request for post {customer_id} successful.")
    else:
        print(f"DELETE request failed with status code: {response.status_code}")

# Execute the examples
get_customers()
id = create_customer()
get_customer(id)
update_customer(id)
delete_customer(id)