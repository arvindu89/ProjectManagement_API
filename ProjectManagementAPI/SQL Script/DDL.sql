create table dbo.ParentTask
( 
  Parent_ID int NOT NULL IDENTITY(1,1),
  Parent_Task nvarchar(150) NOT NULL,
  CONSTRAINT ParentTask_pk PRIMARY KEY (Parent_ID)
);


create table dbo.Project
( 
  Project_ID int NOT NULL IDENTITY(1,1), 
  Project nvarchar(150) NOT NULL,
  Start_Date datetime null,
  End_Date datetime null,
  Priority int null,
  CONSTRAINT Project_pk PRIMARY KEY (Project_ID)
);

create table dbo.Task
( 
  Task_ID int NOT NULL IDENTITY(1,1),
  Parent_ID int NULL,
  Project_ID int NULL,
  Task nvarchar(150) NOT NULL,
  Start_Date datetime null,
  End_Date datetime null,
  Priority int null,
  Status bit not null,
  CONSTRAINT fk_parent_id
    FOREIGN KEY (Parent_ID)
    REFERENCES ParentTask (Parent_ID),
  CONSTRAINT fk_parentTask_id
    FOREIGN KEY (Project_ID)
    REFERENCES dbo.Project (Project_ID),
  CONSTRAINT Task_pk PRIMARY KEY (Task_ID)
);


create table dbo.[User]
( 
  User_ID int NOT NULL IDENTITY(1,1), 
  First_Name nvarchar(150) NOT NULL,
  Last_Name nvarchar(150) NOT NULL,
  Employee_ID nvarchar(30) NOT NULL,
  Project_ID int NULL,
  Task_ID int NULL,
  CONSTRAINT fk_parentUser_id
    FOREIGN KEY (Project_ID)
    REFERENCES dbo.Project (Project_ID),
  CONSTRAINT fk_TaskUser_id
    FOREIGN KEY (Task_ID)
    REFERENCES dbo.Task (Task_ID),
  CONSTRAINT UserID_pk PRIMARY KEY (User_ID)
);