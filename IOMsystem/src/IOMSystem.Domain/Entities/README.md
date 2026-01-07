# Database Structure & Functionalities

This document explains the database structure, entities, relationships, and core functionalities of the IOMSystem domain layer.

## Overview

The IOMSystem domain model is built using Entity Framework Core. It consists of `Sales`, `Inventory`, and `User Management` modules. The design uses a normalized schema with extensive use of Data Annotations for validation and schema definition.

## Entities

### 1. User Management

#### `User` (Table: `Users`)
Represents a system user.
- **Key**: `UserId` (int, Identity)
- **Properties**:
  - `UserEmail`: Unique email address (Index: Unique).
  - `PasswordHash`, `PasswordSalt`: for secure authentication.
  - `BranchCode`: Foreign key linking to the `Branch`.
  - `RoleId`: Foreign key linking to the `Role`.
  - `IsActive`: Soft delete/deactivation flag.
  - `FullName`, `PhoneNumber`: Profile details.
- **Relationships**:
  - Belongs to a **Branch**.
  - Has a **Role**.

#### `Role` (Table: `Roles`)
Defines user permissions and roles (e.g., Admin, Manager).
- **Key**: `RoleId` (int, Identity)
- **Properties**:
  - `RoleName`: Name of the role (e.g., "Admin").
  - `Description`: Optional description.
- **Relationships**:
  - Has many **Users**.

#### `UserRegistrationRequest` (Table: `UserRegistrationRequests`)
Manages new user sign-up requests requiring approval.
- **Key**: `RequestId` (int)
- **Properties**:
  - `UserEmail`, `PasswordHash`, `PasswordSalt`: New user credentials.
  - `BranchCode`: Target branch for the new user.
  - `Status`: Current status (e.g., "Pending", "Approved", "Rejected").
  - `ActionByUserId`: ID of the admin who approved/rejected the request.
  - `RejectionReason`: Reason if rejected.
- **Relationships**:
  - Links to a target **Branch**.

### 2. Branch & Inventory Management

#### `Branch` (Table: `Branches`)
Represents a physical or logical branch of the organization.
- **Key**: `BranchCode` (string, Manual Input)
- **Properties**:
  - `BranchName`: Display name.
  - `Address`, `City`, `State`, `PhoneNumber`: Location details.
  - `IsActive`: Operational status.
- **Relationships**:
  - Has many **Users**.
  - Has many **Stationaries** (Inventory locations).
  - Has many **UserRegistrationRequests**.

#### `Stationary` (Table: `Stationaries`)
Represents a storage location or category within a branch (e.g., "Main Shelf", "Back Store").
- **Key**: `LocationCode` (string)
- **Properties**:
  - `Description`: Details about the location.
  - `BranchCode`: The branch this stationary belongs to.
- **Relationships**:
  - Belongs to a **Branch**.
  - Has many **Products**.

#### `Product` (Table: `Products`)
Represents an item available in the inventory.
- **Key**: `SKU` (string)
- **Properties**:
  - `ProductName`: Name of the item.
  - `ExternalProductCode`: Alternative code/barcode.
  - `LocationCode`: Where the product is stored (links to Stationary).
  - `Price`: Unit price (decimal).
  - `StockQuantity`: Current on-hand quantity.
  - `MinStockThreshold`: Alert level for low stock.
- **Relationships**:
  - Belongs to a **Stationary** location.

### 3. Sales & Orders

#### `Order` (Table: `Orders`)
Represents a sales transaction.
- **Key**: `OrderId` (int)
- **Properties**:
  - `UserId`: The user (staff) who processed the order.
  - `OrderDate`: Timestamp of the order.
  - `TotalAmount`: Grand total of the order.
  - `Status`: Order status (e.g., "Completed", "Cancelled").
  - `Notes`: Optional remarks.
- **Relationships**:
  - Processed by a **User**.
  - Contains many **OrderItems**.

#### `OrderItem` (Table: `OrderItems`)
Represents a line item within an order.
- **Key**: `OrderItemId` (int)
- **Properties**:
  - `OrderId`: Link to the parent order.
  - `SKU`: Product identifier.
  - `ProductName`: Snapshot of the product name at time of order.
  - `Quantity`: Number of units sold.
  - `UnitPrice`: Snapshot of price at time of order.
  - `Subtotal`: `Quantity * UnitPrice`.
- **Relationships**:
  - Belongs to an **Order**.

## Key Relationships Diagram

- **Branch** 1 <---> * **User**
- **Branch** 1 <---> * **Stationary**
- **Stationary** 1 <---> * **Product**
- **User** 1 <---> * **Order**
- **Order** 1 <---> * **OrderItem**
- **Role** 1 <---> * **User**

## System Functionalities

Based on the database structure, the system supports:

1.  **Multi-Branch Support**:
    - Data is segregated or organized by `BranchCode`.
    - Users are assigned to specific branches.

2.  **Inventory Management**:
    - Products are tracked by `SKU`.
    - Stock levels (`StockQuantity`) can be managed.
    - Low stock alerts can be generated using `MinStockThreshold`.
    - Products are organized into `Stationaries` (Locations) within branches.

3.  **Role-Based Access Control (RBAC)**:
    - Users are assigned `Roles` which can dictate permissions in the application layer.
    - New access can be requested via `UserRegistrationRequest` flow, allowing for an approval workflow before creating a `User`.

4.  **Sales Processing**:
    - Orders capture the transaction details, including the staff member (`UserId`) responsible.
    - `OrderItems` store a snapshot of product data (`ProductName`, `UnitPrice`) to preserve historical accuracy even if product details change later.
