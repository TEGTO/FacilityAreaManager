## Overview

This guide will help you get started with the **Facility Area Manager API**, hosted on Microsoft Azure and integrated with AzureSQL. Follow the steps below to access the application, configure the environment, and use the available endpoints.

---

## Web Application

- **Live Application**:  
  [https://facilityareamanager-api-germanywestcentral-001.azurewebsites.net/](https://facilityareamanager-api-germanywestcentral-001.azurewebsites.net/)  

- **AzureSQL Connection String**:  
  [Download Connection String](https://drive.google.com/file/d/1r57VSL2mzH42tOpJYASOBOvCJ8hpmmVU/view?usp=sharing)  

- **API Key**:  
  [Download API Key](https://drive.google.com/file/d/10aht7N6CQiIEtFpLwkZ4uV_zYvCiB3Nd/view?usp=sharing)  

---

## Authentication

To use the API, include the API Key in the request headers as follows:

### Example Header
```http
X-Api-Key: apikeyvalue
```

### Header Configuration Screenshot:
![API Key Example](https://github.com/user-attachments/assets/6e5348ae-cf97-4a15-abd3-24ddadd5d690)

---

## Endpoints

### 1. **Create a New Facility Contract**
   **Method**: `POST`  
   **Endpoint**: `/facilitycontracts/contract`  
   **Request Body**:
   ```json
   {
     "productionFacilityCode": "string",
     "processEquipmentTypeCode": "string",
     "equipmentQuantity": 0
   }
   ```

### 2. **Create a New Equipment**
   **Method**: `POST`  
   **Endpoint**: `/facilitycontracts/equipment`  
   **Request Body**:
   ```json
   {
     "name": "string",
     "area": 0
   }
   ```

### 3. **Create a New Facility**
   **Method**: `POST`  
   **Endpoint**: `/facilitycontracts/facility`  
   **Request Body**:
   ```json
   {
     "name": "string",
     "standardAreaForEquipment": 0
   }
   ```

### 4. **Get All Contracts**
   **Method**: `GET`  
   **Endpoint**: `/facilitycontracts/contracts`

---

## Installation Guide

### 1. **Clone the Repository**
   Run the following command to clone the repository:
   ```bash
   git clone https://github.com/TEGTO/FacilityAreaManager
   ```

### 2. **Navigate to the Project Directory**
   Move to the backend directory and open the solution file:
   ```bash
   cd src/FacilityAreaManager.Backend
   ```
   Open the `.sln` file in your preferred IDE (e.g., Visual Studio).

### 3. **Set Up the Environment**
   - Download and configure the [.env file](https://drive.google.com/file/d/1_eXYU8U9qy5o5wG4yw5ymWkk1oFrhU_0/view?usp=sharing).  
   - Or create one manually with the required environment variables.

### 4. **Run the Project**
   Run the Docker Compose project directly from Visual Studio.

### 5. **Access Swagger API Documentation**
   Once the application is running, open your browser and navigate to:  
   [http://localhost:7145/swagger/index.html](http://localhost:7145/swagger/index.html)
