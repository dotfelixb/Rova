-- create rova db and switch to it

-- CREATE DATABASE rova;

CREATE TABLE IF NOT EXISTS Install
(
	id UUID PRIMARY KEY	
	, displayname VARCHAR(100)
	, enabled BOOLEAN default(FALSE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS Modules
(
	id UUID PRIMARY KEY	
	, displayname VARCHAR(100)
	, uri varchar(100) NOT null
	, enabled BOOLEAN default(FALSE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS InstallModule
(
	id UUID PRIMARY KEY	
	, displayname VARCHAR(100)
	, enabled BOOLEAN default(FALSE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS "user"
(
	id UUID PRIMARY KEY	
	, username VARCHAR(50) unique
	, displayname VARCHAR(100)
	, email VARCHAR(100) unique
	, passwordhash TEXT
	, enabled BOOLEAN DEFAULT(TRUE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

INSERT INTO "user"(id, username, displayname, email, passwordhash, enabled, createdby, updatedby) 
VALUES(
	'00000000-0000-0000-0000-000000000001', 
	'root', 
	'Root',
	'root@app.com',
	'', 
	TRUE, 
	'00000000-0000-0000-0000-000000000001', 
	'00000000-0000-0000-0000-000000000001'
);

CREATE TABLE IF NOT EXISTS UserAuditLog 
(
	id UUID PRIMARY KEY
	, targetId UUID
	, actionname VARCHAR(50) 
	, objectname VARCHAR(50)
	, objectdata JSONB
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS "role"
(
	id UUID PRIMARY KEY	
	, rolename VARCHAR(50) UNIQUE
	, enabled BOOLEAN default(TRUE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

INSERT INTO "role" (id, rolename, enabled, createdby, updatedby)
VALUES(
	'00000000-0000-0000-0000-000000000001', 
	'root', 
	TRUE, 
	'00000000-0000-0000-0000-000000000001', 
	'00000000-0000-0000-0000-000000000001'
);

CREATE TABLE IF NOT EXISTS RoleAuditLog 
(
	id UUID PRIMARY KEY
	, targetId UUID
	, actionname VARCHAR(50) 
	, objectname VARCHAR(50)
	, objectdata JSONB
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS UserRoles
(
	userid UUID 
	, roleid UUID 
	, enabled BOOLEAN DEFAULT(FALSE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

ALTER TABLE UserRoles ADD PRIMARY KEY (userid, roleid);

INSERT INTO userroles (userid, roleid, enabled, createdby, updatedby)
VALUES(
	'00000000-0000-0000-0000-000000000001', 
	'00000000-0000-0000-0000-000000000001', 
	TRUE, 
	'00000000-0000-0000-0000-000000000001', 
	'00000000-0000-0000-0000-000000000001'
);

CREATE TABLE IF NOT EXISTS UserRolesAuditLog 
(
	id UUID PRIMARY KEY
	, targetId UUID
	, actionname VARCHAR(50) 
	, objectname VARCHAR(50)
	, objectdata JSONB
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS Customer 
(
	id UUID PRIMARY KEY
	, code VARCHAR(20) 
	, title VARCHAR(10)
	, firstname VARCHAR(50)
	, lastname VARCHAR(50)
	, birthat DATE
	, gender VARCHAR(20)
	, displayname VARCHAR(100)
	, company VARCHAR(50)
	, phone VARCHAR(20)
	, mobile VARCHAR(20)
	, website VARCHAR(50)
	, email VARCHAR(30)
	, fromlead UUID
	, customertype VARCHAR(15)
	, subcustomer BOOLEAN
	, parentcustomer UUID
	, billparent BOOLEAN
	, isinternal BOOLEAN
	, billingstreet VARCHAR(50)
	, billingcity VARCHAR(50)
	, billingstate VARCHAR(50)
	, shippingstreet VARCHAR(50)
	, shippingcity VARCHAR(50)
	, shippingstate VARCHAR(50)
	, shipbilling BOOLEAN
	, preferredmethod VARCHAR(20)
	, preferreddelivery VARCHAR(20)
	, openingbalance DECIMAL(18, 4)
	, openingbalanceat TIMESTAMPTZ
	, taxid VARCHAR(50)
	, taxexempted BOOLEAN
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS CustomerAuditLog 
(
	id UUID PRIMARY KEY
	, targetId UUID
	, actionname VARCHAR(50) 
	, objectname VARCHAR(50)
	, objectdata JSONB
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
);

CREATE SEQUENCE CustomerCode INCREMENT 1 START 1;

CREATE TABLE IF NOT EXISTS Lead 
(
	id UUID PRIMARY KEY
	, code VARCHAR(20) 
	, title VARCHAR(10)
	, firstname VARCHAR(50)
	, lastname VARCHAR(50)
	, birthat DATE
	, gender VARCHAR(20)
	, displayname VARCHAR(100)
	, company VARCHAR(50)
	, phone VARCHAR(20)
	, mobile VARCHAR(20)
	, website VARCHAR(50)
	, email VARCHAR(30)
	, campaign UUID
	, status VARCHAR(30)
	, leadtype VARCHAR(15)
	, addresstype VARCHAR(50)
	, addressstreet VARCHAR(50)
	, addresscity VARCHAR(50)
	, addressstate VARCHAR(50) 
	, marketsegment VARCHAR(50) 
	, industry VARCHAR(50) 
	, subscribed BOOLEAN DEFAULT(TRUE)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS LeadAuditLog 
(
	id UUID PRIMARY KEY
	, targetId UUID
	, actionname VARCHAR(50) 
	, objectname VARCHAR(50)
	, objectdata JSONB
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
);

CREATE SEQUENCE LeadCode INCREMENT 1 START 1;

CREATE TABLE IF NOT EXISTS Campaign 
(
	id UUID PRIMARY KEY
	, code VARCHAR(20) 
	, displayname VARCHAR(100)
	, location VARCHAR(250)
	, deleted BOOLEAN DEFAULT(FALSE)
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
	, updatedby UUID
	, updatedat TIMESTAMPTZ DEFAULT(now())
);

CREATE TABLE IF NOT EXISTS CampaignAuditLog 
(
	id UUID PRIMARY KEY
	, targetId UUID
	, actionname VARCHAR(50) 
	, objectname VARCHAR(50)
	, objectdata JSONB
	, createdby UUID
	, createdat TIMESTAMPTZ DEFAULT(now())
);

CREATE SEQUENCE CampaignCode INCREMENT 1 START 1;

