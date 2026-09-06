#   ChurchOS API

## Description
- This is a Rolebased  System Architecture for A church operations , it enables the creation of multiple-branch church account under a single Head church.
- It enables the church branch to create and have their departments-unit and onboarding of the church members.
- The System serves every members across all branches the welcoming messages and also every outgoing announcement for every weekly Church services and distributed across all members through the Super Admin panel
- It enables the individual church member account creation , but branch admin are only created by the Super admin.
- Each Branch Admins can add workers(member) to the available department units  

## System Design Architecture 
- Monolithic system design approach is adopted , here all the System Services and components are couple together under this single code-base repository and are seperated from folders to folders;

## System Components 

- Progressively, the following are the available system components 
    - Rolebased Authentication & Authorization
    - Postgre Database Context
    - REST APIs Route Endpoints
    - Termii SMS provider
    - Core System Services
    - Background-worker


## How to use the System 
 - The Super Admin Entity has been seeded by default , with which all  (read and write ) permission is granted, the super admin only can creates new branch and branch admin.
 - Every branch admin with Login credentials can create the department units and add members working for every units 
 - Members can create their user account with which only read permission is granted 
 - When branch admins receives new members/ onboard new members records, the system sends a welcome message to the , and every subsequent announcements going out from the Headquarters are spread across to all members.


 ## API ENDPOINTS
 - Production (https://churchos-t0qb.onrender.com)[BASEURL]

 - POST /api/member/new =>  Registration of new members
  {
    "Name": "John Doe",
    "Phone": "2348012345678",
    "RoleID": 3,
    "Email": "john@doe.com",
    "Password": "password"
  }
 - POST  /api/member/login =>  Login routes for registered accounts
   {
    Email="user@account.com"
    Password="Password"
   }

 - GET /api/member/all => Read all members across all branches [Accessible to Only Admins]

 - POST api/branch/admin/new => Creation of new branch and Branch Admin [Accessible to Only the Super Admin]
   {
    "Name": "HTM Ijaiye Ojokoooro",
    "Phone": "2348012345678",
    "Branch": "Ijaiye Branch Zonal 1",
    "RoleID": 2,
    "Email": "admin@account.com",
    "Password": "password"
   }

- GET api/branch/department/4/workers?page=1&pageSize=20 => Reading of workers in particular departments [Accessible to Only Admins]

- POST /api/branches/new/member => Onboarding of new members by any branch admin [Accessible to Branch admins]

- POST /api/branches/department/create => Creation of department unit [Accessible to all Admins]

- POST api/branches/department/onboard/worker => Mapping of members to work department units [Accessible to all Admins]

- POST api/announcement/announce => Broadcast church prgrams announcements across every branch members [Accessible only to super admins]

