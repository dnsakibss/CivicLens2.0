# 🏛️ CivicLens 2.0

> A civic complaint management and community news platform built with C# Windows Forms and SQL Server — empowering citizens to report issues and enabling authorities to respond effectively.

---

## 📌 About

**CivicLens** is a desktop application that bridges the gap between citizens and local authorities. Citizens can submit complaints about civic issues (infrastructure, safety, public services, etc.), track their resolution, and engage with a community newsfeed. Authorities across multiple roles — Admin, Moderator, Police, and Journalist — each get a tailored dashboard to manage, investigate, and report on those issues.

**CivicLens 2.0** is a major solo update with a fully redesigned UI, real-time per-complaint chat, media attachment support, a paginated newsfeed with comments, expanded role system, and significant admin tooling improvements.

---

## 🔗 Repositories

| Version | Repository |
|---|---|
| CivicLens (v1) | [github.com/firojkoraishisourov/CivicLens](https://github.com/firojkoraishisourov/CivicLens) |
| CivicLens 2.0 | *(this repository)* |

---

## ✨ Full Feature List

### 🔐 Authentication & Registration

- Role-based login with username and password
- Show/hide password toggle on the login screen
- New user self-registration with full name, email, phone, address, and role selection
- All new registrations are placed in **Pending** status and require admin approval before login
- Duplicate username check enforced at signup
- Separate `Logins` table for credentials, `Users` table for profile data

### 👤 User Profiles

- View own profile: full name, email, phone, address, role, registration date, approval date
- Edit profile details (name, email, phone, address)
- Change password via a dedicated Update Password form
- Schema-adaptive profile loader — gracefully handles variations in column names (e.g. `Address` vs `AddressLine`)

### 🏠 Role-Based Dashboard

Every user lands on a shared Dashboard that shows a personalised welcome message and role label, then reveals only the panel of actions relevant to their role:

| Role | Dashboard Actions Available |
|---|---|
| **Admin** | Manage Users, Manage Admins, Manage Categories, Manage Locations, User Approvals, View Profile, Edit Profile, Change Password, Newsfeed, Chat |
| **Moderator** | Complaint Queue, Newsfeed, View Profile, Edit Profile, Change Password |
| **Police** | Assigned Complaints, Newsfeed, View Profile, Edit Profile, Change Password |
| **Journalist** | Journalist Feed, Newsfeed, View Profile, Edit Profile, Change Password |
| **Citizen** | Submit Complaint, My Complaints, Newsfeed, View Profile, Edit Profile, Change Password |

---

### 📋 Complaint Submission (Citizen)

- Submit a new complaint with:
  - **Title** and **Description**
  - **Category** (loaded dynamically from DB)
  - **Priority** (Low / Medium / High / Critical)
  - **Location** — cascading dropdowns: District → City → Area (loaded from DB)
  - **Media attachments** — attach multiple files (images, documents), mark one as primary, set sort order
- Submission guard: prevents double-submit with a `_isSubmitting` flag

### 📂 My Complaints (Citizen)

- Grid view of all complaints submitted by the logged-in citizen
- Columns: ID, Title, Category, Status, Date
- Search/filter by title, status, or category
- **View** button — opens complaint detail in read-only mode
- **Edit** button — opens complaint detail in editable mode (citizen can update title, description, priority)
- Grid refreshes automatically after a successful edit

### 🔍 Complaint Detail & Timeline

- Displays all complaint metadata: title, category, priority, status, location, description, creation date
- **Media gallery** — loads and displays all attached media files
- **Full status timeline** — shows every status change with old status, new status, note, who changed it, and when
- Edit mode: allows updating title, description, and priority (status and location are always read-only)

---

### 🛂 Moderator — Complaint Queue

- Full grid of all complaints in the system
- Filters: keyword search (title/status), category dropdown, status dropdown
- **"Only Unassigned"** checkbox — narrows list to `New` or `Pending` complaints
- **View** button — opens read-only complaint detail
- **Assign** button — opens the assignment dialog

#### Complaint Assignment

- Moderator selects a **target role** (Police or Journalist) and a specific **assignee** from active, approved users in that role
- Optional **assignment note**
- On confirm, the system (in a single SQL transaction):
  1. Deactivates any existing active assignment for that complaint
  2. Inserts a new `Assignments` record
  3. Promotes complaint status to `Assigned` (only if currently `New` or `Pending`)
  4. Writes a `StatusHistory` entry

---

### 🚔 Police — Assigned Complaints

- Grid of all complaints actively assigned to the logged-in police officer
- Filters: keyword search, status dropdown (`Assigned`, `InProgress`, `OnHold`, `Resolved`, `Rejected`, `Closed`)
- **View** button — read-only complaint detail
- **Update Status** button — opens status update form
- **Chat** button — opens the per-complaint chat thread

### 📰 Journalist — Assigned Feed

- Grid of all complaints actively assigned to the logged-in journalist
- Filters: keyword search, status dropdown (includes `Covered`)
- **View** button — read-only complaint detail
- **Mark as Covered** button — prompts for an optional note, then:
  - Updates complaint status to `Covered`
  - Inserts a `StatusHistory` record
- **Chat** button — opens the per-complaint chat thread

---

### 🔄 Status Update

- Authorities can move a complaint through any of: `Assigned` → `InProgress` → `OnHold` → `Resolved` → `Rejected` → `Closed`
- Defaults the "new status" intelligently (e.g., `InProgress` when current is `Assigned`)
- Optional timestamped note saved alongside the status change
- Both `Complaints` and `StatusHistory` tables are updated together

---

### 💬 Per-Complaint Chat

- Every complaint has its own chat thread accessible to the assigned officer/journalist and the submitting citizen
- Messages stored in `ComplaintMessages` table with sender ID, receiver ID, and timestamp
- **Auto-refresh timer** — polls the database every few seconds for new messages, updating `_lastMessageId` to fetch only new ones
- Role-aware display: messages are styled differently based on sender role

---

### 📰 Public Newsfeed

- Paginated feed (10 items per page) showing public complaint updates
- Status filter dropdown: `All`, `Pending`, `Assigned`, `InProgress`, `OnHold`, `Resolved`, `Rejected`, `Closed`
- Role-aware card rendering:
  - Privileged users (Admin, Moderator, Police) see additional internal metadata
  - Citizens and Journalists see a public-facing view
- **Load More** pagination — fetches the next page without replacing existing cards
- Colour-coded status badges (green for Resolved, red for Rejected, amber for OnHold, etc.)
- Clicking a newsfeed item opens the comment thread for that post
- **Comments** — users can read and post comments on any newsfeed entry

---

### 🛠️ Admin — User Management

- Full grid of all users in the system
- Filters: keyword search (name/email/username), role dropdown, active/inactive status
- Bulk actions: **Activate**, **Deactivate**, **Delete** selected users
- Per-row actions: activate/deactivate toggle, delete
- Cascade-safe delete: removes all dependent records (complaints, assignments, messages, reactions, status history, media) before deleting the user

### ✅ Admin — User Approvals

- Dedicated queue showing all users with `ApprovalStatus = 'Pending'`
- Filters: keyword search (name, email, phone), role dropdown
- Per-row **Approve** and **Reject** buttons
- Bulk **Approve Selected** and **Reject Selected** buttons
- Approval records the approving admin ID and timestamp (`ApprovedAt`, `ApprovedByAdminId`)

### 🏷️ Admin — Category Management

- Create, edit, and delete complaint categories
- Optional **Description** field and **IsActive** toggle (both gracefully disabled if the column doesn't exist in the DB)
- **Safety guard on delete**: checks if any complaints use the category; blocks deletion if in use and shows a clear error message
- Keyword search across name and description
- Schema-adaptive: detects available columns at runtime

### 📍 Admin — Location Management

- Manage the District / City / Area location hierarchy used in complaint submission
- Full CRUD: add, edit, delete location entries
- Keyword search across location fields

### 👮 Admin — Manage Admins

- Separate form for managing administrator-level accounts
- Add, edit, deactivate, or remove admin users independently of the main user list

---

## 🗄️ Database Tables Referenced

| Table | Purpose |
|---|---|
| `Users` | User profiles and approval status |
| `Logins` | Login credentials (username / password) |
| `Roles` | Role definitions (Admin, Moderator, Police, Journalist, Citizen) |
| `Complaints` | Complaint records |
| `Categories` | Complaint categories |
| `Locations` | District / City / Area hierarchy |
| `Assignments` | Tracks who a complaint is assigned to (with IsActive flag) |
| `StatusHistory` | Full audit trail of every status change |
| `ComplaintMedia` | Media files attached to complaints |
| `ComplaintMessages` | Per-complaint chat messages |
| `ComplaintReactions` | Reactions on complaints/newsfeed items |
| `NewsfeedComments` | Comments on newsfeed posts |

---

## 🏗️ Tech Stack

| Layer | Technology |
|---|---|
| Language | C# (.NET Framework 4.7.2) |
| UI Framework | Windows Forms (WinForms) |
| Database | Microsoft SQL Server (SQL Express) |
| Data Access | ADO.NET (`System.Data.SqlClient`) |
| IDE | Visual Studio 2019+ |
| DB Backup Format | `.bacpac` (SQL Server / Azure compatible) |

---

## 🚀 Getting Started

### Prerequisites

- Windows OS
- [Visual Studio 2019+](https://visualstudio.microsoft.com/)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or full SQL Server)
- .NET Framework 4.7.2

### Setup

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd CivicLens
   ```

2. **Restore the database**
   - Open **SQL Server Management Studio (SSMS)**
   - Import `CivicLensDB.bacpac`:
     `Right-click Databases → Import Data-tier Application → select CivicLensDB.bacpac`

3. **Update the connection string**
   - Open each `.cs` form file and replace the connection string with your SQL Server instance name:
     ```csharp
     new SqlConnection("Data Source=YOUR_SERVER\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");
     ```
   - The connection string appears at the top of every form file.

4. **Build and run**
   - Open `CivicLens.sln` in Visual Studio
   - Press `F5` or click **Start**

---

## 📁 Project Structure

```
CivicLens/
├── CivicLens.sln
└── CivicLens/
    ├── Program.cs                          # Entry point
    ├── App.config
    │
    ├── — Auth & Registration —
    ├── LoginForm.cs                        # Login, show/hide password, role routing
    ├── SignupForm.cs                       # Self-registration with pending approval
    │
    ├── — Dashboard —
    ├── DashboardForm.cs                    # Role-aware central navigation hub
    │
    ├── — Complaints —
    ├── SubmitComplaintForm.cs              # Submit with category, priority, location, media
    ├── MyComplaintsForm.cs                 # Citizen's own complaints (view + edit)
    ├── ComplaintDetailForm.cs              # Detail view with media gallery + timeline
    ├── AssignComplaintForm.cs              # Moderator: assign to Police or Journalist
    ├── ModeratorQueueForm.cs               # Moderator: full complaint queue with filters
    ├── UpdateStatusForm.cs                 # Authority: status transition with note + timestamp
    ├── PoliceAssignedComplaintsForm.cs     # Police: personal assigned complaint list
    ├── JournalistFeedForm.cs               # Journalist: assigned feed + mark covered
    │
    ├── — Chat —
    ├── ChatForm.cs                         # Per-complaint real-time chat with auto-refresh
    │
    ├── — Newsfeed —
    ├── NewsfeedForm.cs                     # Paginated public newsfeed with status filters
    ├── NewsfeedcommentsForm.cs             # Comments on newsfeed entries
    │
    ├── — Profile —
    ├── ViewProfileForm.cs                  # View user profile (schema-adaptive)
    ├── EditProfileForm.cs                  # Edit name, email, phone, address
    ├── UpdatePasswordForm.cs               # Change password
    │
    ├── — Admin —
    ├── AdminUsersForm.cs                   # Manage all users (bulk actions, cascade delete)
    ├── AdminManageAdminsForm.cs            # Manage admin accounts separately
    ├── AdminUserApprovalsForm.cs           # Approve / reject pending registrations
    ├── AdminCategoriesForm.cs              # CRUD categories (schema-adaptive, safe delete)
    ├── AdminLocationsForm.cs               # CRUD District / City / Area hierarchy
    │
    ├── CivicLensDB.bacpac                  # Full database backup
    └── Properties/                         # Assembly info, resources, settings
```

---

## 🆕 What's New in CivicLens 2.0

CivicLens 2.0 is a complete overhaul of the original, rebuilt and extended solo by [Nazmus Sakib Sami](https://www.linkedin.com/in/nazmussakibsami/).

| Area | CivicLens v1 | CivicLens 2.0 |
|---|---|---|
| **UI Design** | Basic WinForms layout | Custom-painted panels, gradient branding, colour-coded badges |
| **Chat** | Not available | Per-complaint real-time chat with auto-refresh timer |
| **Media Attachments** | Not available | Multi-file media attach with primary selection and sort order |
| **Newsfeed** | Basic feed | Paginated feed (10/page), status filters, role-aware card rendering |
| **Comments** | Not available | Comment threads on newsfeed posts |
| **Complaint Timeline** | Not available | Full `StatusHistory` audit trail on every complaint |
| **Assignment System** | Basic | Transactional assignment: deactivates old, inserts new, logs history |
| **Journalist Role** | Not available | Full journalist flow with assigned feed and "Mark as Covered" action |
| **Police Chat** | Not available | Chat button directly in Police assigned complaints list |
| **Admin — Categories** | Basic CRUD | Schema-adaptive CRUD, active/inactive toggle, safe-delete guard |
| **Admin — User Management** | Basic list | Bulk activate / deactivate / delete, cascade-safe deletion |
| **Admin — Approvals** | Not available | Dedicated approval queue with bulk approve/reject |
| **Registration** | Not available | Full self-registration form with duplicate-check and pending status |
| **Password Management** | Not available | Dedicated Update Password form |
| **Profile System** | Not available | View and edit profile, schema-adaptive column detection |
| **Status Workflow** | Limited | Full workflow: `Pending` → `Assigned` → `InProgress` → `OnHold` → `Resolved` / `Rejected` / `Closed` / `Covered` |
| **Location Hierarchy** | Flat | Cascading District → City → Area dropdowns in complaint submission |
| **DB Schema Safety** | Assumed fixed | Runtime column detection across Users, Categories, and Locations tables |

---

## 👥 Authors

**CivicLens (v1)** — Built by:

- [Nazmus Sakib Sami](https://www.linkedin.com/in/nazmussakibsami/)
- [MD. Firoj Koraishi Sourov](https://www.linkedin.com/in/md-firoj-koraishi-sourov-7279472a4/?skipRedirect=true)

**CivicLens 2.0** — Updated and extended solo by:

- [Nazmus Sakib Sami](https://www.linkedin.com/in/nazmussakibsami/)

---

## 📄 License

Copyright 2026 [Nazmus Sakib Sami](https://www.linkedin.com/in/nazmussakibsami/)

Licensed under the **Apache License, Version 2.0**. You may not use this project except in compliance with the License.

You may obtain a copy of the License at:

> http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an **"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND**, either express or implied. See the [LICENSE](./LICENSE) file for the full terms governing permissions and limitations.
