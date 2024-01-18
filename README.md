<div>
<h1> Expense Tracker Project </h2>
<img src="https://assets.materialup.com/uploads/1eae3002-ea83-4b83-bb8c-e844400e7f83/preview.jpg" width="128"/>
</div>

### Overview

This .NET-based application is designed to help users manage and track their expenses and incomes effectively. The goal is to provide a comprehensive solution for organizing financial transactions, generating reports, and offering additional premium features for enhanced user experience.

## Postman API documentation
<a href="https://documenter.getpostman.com/view/21619259/2s9YsRd9TF#757dd6bd-9a08-40fd-b5f9-7b19dfaf9b81" target="_blank">Click here</a>

## Functionalities

1. **Dashboard**
   - Display current amount
   - Show last 5 expense changes
   - Show last 5 income changes
   - Buttons to navigate to expense and income group list pages
   - Button to add expense/income through a popup

2. **CRUD Operations**
   - Create, Read, Update, and Delete expenses
   - Create, Read, Update, and Delete incomes
   - Create, Read, Update, and Delete income groups
   - Create, Read, Update, and Delete expense groups

## Database Models

- **Expense Group**
  - Name
  - Description

- **Expense**
  - Description
  - Amount
  - Expense group

- **Income Group**
  - Name
  - Description

- **Income**
  - Description
  - Amount
  - Income group

- **Reminder** (Premium Feature)
  - Reminder day
  - Type
  - Active

## User Interface

1. **Dashboard**
   - Total amount widget
   - Last 5 expense changes
   - Last 5 income changes
   - Buttons to navigate to expense and income group list pages
   - Button to add expense/income through a popup

2. **Income/Expense Group List Page**
   - Table with group details
   - Edit and delete buttons
   - Details button navigating to details page
   - Button to add a group through a popup

3. **Income/Expense Group Details Page**
   - Name
   - Description
   - Last 5 account changes for that group

4. **Income/Expense Edit Popup**
   - Editable name field
   - Editable description field
   - Save button
   - Cancel button

5. **Income/Expense List Page**
   - Table
     - No.
     - Group name
     - Amount
     - Description
     - Creation time
     - Edit and delete buttons
   - Button to add income/expense through popup
   - Incomes table with option to filter and a button to get the report
   - Expenses table with option to filter and a button to get the report

6. **Reminder Settings (Premium Feature inside Profile Settings)**
   - Button to create reminder through a popup
   - Details of currently set reminder on the dashboard

7. **Blog Page (Premium Feature)**