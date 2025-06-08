-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2025-06-07 16:03:51.664

-- tables
-- Table: Role
CREATE TABLE Role (
    role_id int  NOT NULL IDENTITY,
    role_name nvarchar(20)  NOT NULL,
    CONSTRAINT Role_pk PRIMARY KEY  (role_id)
);

-- Table: Ticket
CREATE TABLE Ticket (
    ticket_id int  NOT NULL IDENTITY,
    title nvarchar(150)  NOT NULL,
    description nvarchar(max)  NOT NULL,
    created_at datetime  NOT NULL,
    updated_at datetime  NOT NULL,
    user_created int  NOT NULL,
    user_assigned int  NOT NULL,
    category_id int  NOT NULL,
    priority_id int  NOT NULL,
    status_id int  NOT NULL,
    is_deleted bit  NOT NULL DEFAULT 0,
    CONSTRAINT Ticket_pk PRIMARY KEY  (ticket_id)
);

-- Table: TicketAttachment
CREATE TABLE TicketAttachment (
    attachment_id int  NOT NULL IDENTITY,
    ticket_id int  NOT NULL,
    file_name nvarchar(255)  NOT NULL,
    file_path nvarchar(255)  NOT NULL,
    uploaded_at datetime  NOT NULL,
    is_deleted bit  NOT NULL DEFAULT 0,
    CONSTRAINT TicketAttachment_pk PRIMARY KEY  (attachment_id)
);

-- Table: TicketCategory
CREATE TABLE TicketCategory (
    category_id int  NOT NULL IDENTITY,
    name nvarchar(100)  NOT NULL,
    description nvarchar(255)  NOT NULL,
    CONSTRAINT TicketCategory_pk PRIMARY KEY  (category_id)
);

-- Table: TicketComment
CREATE TABLE TicketComment (
    comment_id int  NOT NULL IDENTITY,
    ticket_id int  NOT NULL,
    author_id int  NOT NULL,
    content nvarchar(max)  NOT NULL,
    created_at datetime  NOT NULL,
    is_deleted bit  NOT NULL DEFAULT 0,
    CONSTRAINT TicketComment_pk PRIMARY KEY  (comment_id)
);

-- Table: TicketPriority
CREATE TABLE TicketPriority (
    priority_id int  NOT NULL IDENTITY,
    name nvarchar(20)  NOT NULL,
    CONSTRAINT TicketPriority_pk PRIMARY KEY  (priority_id)
);

-- Table: TicketStatus
CREATE TABLE TicketStatus (
    status_id int  NOT NULL IDENTITY,
    name nvarchar(20)  NOT NULL,
    CONSTRAINT TicketStatus_pk PRIMARY KEY  (status_id)
);

-- Table: User
CREATE TABLE "User" (
    user_id int  NOT NULL IDENTITY,
    name nvarchar(50)  NOT NULL,
    email nvarchar(100)  NOT NULL,
    password_hash nvarchar(max)  NOT NULL,
    created_at datetime  NOT NULL,
    is_deleted bit  NOT NULL DEFAULT 0,
    role_id int  NOT NULL,
    CONSTRAINT User_pk PRIMARY KEY  (user_id)
);

-- foreign keys
-- Reference: FK_Ticket_User_Assigned (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT FK_Ticket_User_Assigned
    FOREIGN KEY (user_assigned)
    REFERENCES "User" (user_id);

-- Reference: FK_Ticket_User_Created (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT FK_Ticket_User_Created
    FOREIGN KEY (user_created)
    REFERENCES "User" (user_id);

-- Reference: TicketAttachment_Ticket (table: TicketAttachment)
ALTER TABLE TicketAttachment ADD CONSTRAINT TicketAttachment_Ticket
    FOREIGN KEY (ticket_id)
    REFERENCES Ticket (ticket_id);

-- Reference: TicketComment_Ticket (table: TicketComment)
ALTER TABLE TicketComment ADD CONSTRAINT TicketComment_Ticket
    FOREIGN KEY (ticket_id)
    REFERENCES Ticket (ticket_id);

-- Reference: TicketComment_User (table: TicketComment)
ALTER TABLE TicketComment ADD CONSTRAINT TicketComment_User
    FOREIGN KEY (author_id)
    REFERENCES "User" (user_id);

-- Reference: Ticket_Priority (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_Priority
    FOREIGN KEY (priority_id)
    REFERENCES TicketPriority (priority_id);

-- Reference: Ticket_Status (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_Status
    FOREIGN KEY (status_id)
    REFERENCES TicketStatus (status_id);

-- Reference: Ticket_TicketCategory (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_TicketCategory
    FOREIGN KEY (category_id)
    REFERENCES TicketCategory (category_id);

-- Reference: User_Role (table: User)
ALTER TABLE "User" ADD CONSTRAINT User_Role
    FOREIGN KEY (role_id)
    REFERENCES Role (role_id);

-- End of file.

