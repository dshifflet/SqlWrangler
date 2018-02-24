# SQL WRANGLER
```
 _____       _ _    _                       _           
/  ___|     | | |  | |                     | |          
\ `--.  __ _| | |  | |_ __ __ _ _ __   __ _| | ___ _ __ 
 `--. \/ _` | | |/\| | '__/ _` | '_ \ / _` | |/ _ \ '__|
/\__/ / (_| | \  /\  / | | (_| | | | | (_| | |  __/ |   
\____/ \__, |_|\/  \/|_|  \__,_|_| |_|\__, |_|\___|_|   
          | |                          __/ |            
          |_|                         |___/             
```

## OVERVIEW:
This is a tool for running SQL and doing amazing things...

It's a windows application.  It looks like this when you are using it...
![alt text](https://raw.githubusercontent.com/dshifflet/SqlWrangler/master/imgs/capture1.png "Example Screen Shot")

_
THIS BASICALLY RUNS ALL THE SQL AS a C# DataCommand WITH the CommandText set to what is highlighted or running.
ALSO IT AUTO COMMITS!  SO BE CAREFUL.  Also you can't use ";" all over the place.  But you can in Anonymous blocks.
_

Imagine if you were setting the sql to something like this in C#

```
command.CommandText = sql;
```

The SQL you run in this thing is that sql variable.   

You are probably best off only running selects in this thing and not insert/update/delete (IUD) statements.  Use something else for IUD like SqlDeveloper or SQL QA or something.

**IT IS AUTOCOMMIT ALWAYS!!!**

_AUTOCOMMIT ALWAYS!!!_

## What can I do with this?
With SQL Wrangler you can...

* Run SQL, get the data in a grid
* Even run SQL against Excel Files
* Export the grid to 
	- CSV
	- JSON
	- XML as a Serialized DataTable (so you can go off and use the serialized datatable somewhere else, without 
	  the DB)
	- Create Inserts for SqlLite.
	- Generate common C# code.  Model, NH Mapping, ADO Materalizer (Hydrator)

* Save snippets of SQL with a name.  (Highlight some SQL, click Snippets-Add Snippet)  (To remove a snippet hold down
  CTRL and click on it)  BACK UP YOUR SNIPPETS!!! (There is a bug where they get whacked for some reason!!!)
* Improve your SQL debugging skills, because if there is a problem all you get back is the .NET exception with
  No Column or Line Number.
* Wonder in amazement at how basic things like FIND and REPLACE don't exist
* Stare into the distance and contemplate how cancelling queries once the data starts streaming back doesn't 
  actually cancel them but replaces the results at some seemingly random point.
* Be baffled by how a SQL error causes all the buttons to disable yet you can still run SQL by using F9. 
	(Hit ESC it will cancel and the buttons will come back now.)

## HOW TO SETUP CONNECTIONS...
Stick them in the file "data-connectionStrings.config"

If you want to prompt for a user name and password do something like.
DATA SOURCE=TNS_SOMETHING;PASSWORD={password};USER ID={username};

SAMPLE _data-connectionStrings.config_
```
<?xml version="1.0" encoding="utf-8" ?>
<connectionStrings>
  <add name="SomeConnection" connectionString="DATA SOURCE=TNS_SAMPLE;PASSWORD={password};USER ID={username};enlist=dynamic;Connection Timeout=60;"/>
</connectionStrings>
```
## HOW TO WORK WITH EXCEL FILES
It uses the Microsoft Access Database Engine 2010 Redistributable to work with Excel files.  You will need to install it.

You should compile the application to the platform version of the redistributable (32bit or 64bit)!  Any CPU may or may not work!

At the logon screen click the button on the lower right hand side with the excel icon.

The SQL is like this:

SELECT * FROM [<WORKSHEET_NAME>$]

aka

SELECT * FROM [MyWorkSheet$]

If you column headers has spaces wrap them in [My Column Name], just like the table names.

## KEYBOARD SHORTCUTS...

F1 = Open a new window
F9 = Runs the whole window of SQL, or the highlighted (selected) piece.

## SNIPPETS
* To save a snippet highlight some SQL, click Snippets-Add Snippet.  To remove a snippet hold down
  CTRL and click on it.
* Make backup copies of your snippets for some reason it will delete them periodically. (BUG)

## EXAMPLES
### Sample JSON Output
```
[{"CUSTOMER_ID":1,"CUSTOMER_CODE":"ALFKI","COMPANY_NAME":"Alfreds Futterkiste","CONTACT_NAME":"Maria Anders","CONTACT_TITLE":"Sales Representative","ADDRESS":"Obere Str. 57","CITY":"Berlin","REGION":null,"POSTAL_CODE":"12209","COUNTRY":"Germany","PHONE":"030-0074321","FAX":"030-0076545"},{"CUSTOMER_ID":2,"CUSTOMER_CODE":"ANATR","COMPANY_NAME":"Ana Trujillo Emparedados y helados","CONTACT_NAME":"Ana Trujillo","CONTACT_TITLE":"Owner","ADDRESS":"Avda. de la Constitución 2222","CITY":"México D.F.","REGION":null,"POSTAL_CODE":"05021","COUNTRY":"Mexico","PHONE":"(5) 555-4729","FAX":"(5) 555-3745"},{"CUSTOMER_ID":3,"CUSTOMER_CODE":"ANTON","COMPANY_NAME":"Antonio Moreno Taquería","CONTACT_NAME":"Antonio Moreno","CONTACT_TITLE":"Owner","ADDRESS":"Mataderos  2312","CITY":"México D.F.","REGION":null,"POSTAL_CODE":"05023","COUNTRY":"Mexico","PHONE":"(5) 555-3932","FAX":null},{"CUSTOMER_ID":4,"CUSTOMER_CODE":"AROUT","COMPANY_NAME":"Around the Horn","CONTACT_NAME":"Thomas Hardy","CONTACT_TITLE":"Sales Representative","ADDRESS":"120 Hanover Sq.","CITY":"London","REGION":null,"POSTAL_CODE":"WA1 1DP","COUNTRY":"UK","PHONE":"(171) 555-7788","FAX":"(171) 555-6750"}]
```

### Code Generated by Clicking "Export to SQLite"

```
/* CREATE TABLE NORTHWIND_CUSTOMER */
create table NORTHWIND_CUSTOMER (
CUSTOMER_ID INT NOT NULL,
CUSTOMER_CODE VARCHAR(5) NOT NULL,
COMPANY_NAME VARCHAR(40) NOT NULL,
CONTACT_NAME VARCHAR(30),
CONTACT_TITLE VARCHAR(30),
ADDRESS VARCHAR(60),
CITY VARCHAR(15),
REGION VARCHAR(15),
POSTAL_CODE VARCHAR(10),
COUNTRY VARCHAR(15),
PHONE VARCHAR(24),
FAX VARCHAR(24)
);

/* INSERT TABLE NORTHWIND_CUSTOMER */
insert into NORTHWIND_CUSTOMER (CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX) values (1, 'ALFKI', 'Alfreds Futterkiste', 'Maria Anders', 'Sales Representative', 'Obere Str. 57', 'Berlin', null, '12209', 'Germany', '030-0074321', '030-0076545');
insert into NORTHWIND_CUSTOMER (CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX) values (2, 'ANATR', 'Ana Trujillo Emparedados y helados', 'Ana Trujillo', 'Owner', 'Avda. de la Constitución 2222', 'México D.F.', null, '05021', 'Mexico', '(5) 555-4729', '(5) 555-3745');
insert into NORTHWIND_CUSTOMER (CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX) values (3, 'ANTON', 'Antonio Moreno Taquería', 'Antonio Moreno', 'Owner', 'Mataderos  2312', 'México D.F.', null, '05023', 'Mexico', '(5) 555-3932', null);
insert into NORTHWIND_CUSTOMER (CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX) values (4, 'AROUT', 'Around the Horn', 'Thomas Hardy', 'Sales Representative', '120 Hanover Sq.', 'London', null, 'WA1 1DP', 'UK', '(171) 555-7788', '(171) 555-6750');
```

### Code Generated by Clicking "Generate C#"
```
using System;
using NHibernate.Mapping.ByCode.Conformist;

namespace TODO
{
/*
select * from northwind.customer where rownum<100
*/

	//MODEL
	public class Customer
	{
		//CUSTOMER
		public int Id { get; set; } //CUSTOMER_ID
		public string CustomerCode { get; set; } //CUSTOMER_CODE
		public string CompanyName { get; set; } //COMPANY_NAME
		public string ContactName { get; set; } //CONTACT_NAME
		public string ContactTitle { get; set; } //CONTACT_TITLE
		public string Address { get; set; } //ADDRESS
		public string City { get; set; } //CITY
		public string Region { get; set; } //REGION
		public string PostalCode { get; set; } //POSTAL_CODE
		public string Country { get; set; } //COUNTRY
		public string Phone { get; set; } //PHONE
		public string Fax { get; set; } //FAX
	}


	//NHIBERNATE MAPPING
	internal class CustomerMapping : ClassMapping<Customer>
	{
		public CustomerMapping()
		{
			//Schema("TODO");
			Table("CUSTOMER");
			Lazy(false);
			Id(prop => prop.Id, map =>
			{
				map.Column("CUSTOMER_ID");
				//map.Generator(Generators.Sequence, gmap => gmap.Params(new {sequence = "SOME_SEQ_ID"}));
			});
			Property(prop => prop.CustomerCode, map => map.Column("CUSTOMER_CODE"));
			Property(prop => prop.CompanyName, map => map.Column("COMPANY_NAME"));
			Property(prop => prop.ContactName, map => map.Column("CONTACT_NAME"));
			Property(prop => prop.ContactTitle, map => map.Column("CONTACT_TITLE"));
			Property(prop => prop.Address, map => map.Column("ADDRESS"));
			Property(prop => prop.City, map => map.Column("CITY"));
			Property(prop => prop.Region, map => map.Column("REGION"));
			Property(prop => prop.PostalCode, map => map.Column("POSTAL_CODE"));
			Property(prop => prop.Country, map => map.Column("COUNTRY"));
			Property(prop => prop.Phone, map => map.Column("PHONE"));
			Property(prop => prop.Fax, map => map.Column("FAX"));
		}
	}


	//BASIC MATERIALIZER
	private class MaterializerCustomer
	{
		private Customer Materialize(DbDataReader reader)
		{
			return new Customer() {
				Id = (int) reader["CUSTOMER_ID"],
				CustomerCode = reader["CUSTOMER_CODE"] as string,
				CompanyName = reader["COMPANY_NAME"] as string,
				ContactName = reader["CONTACT_NAME"] as string,
				ContactTitle = reader["CONTACT_TITLE"] as string,
				Address = reader["ADDRESS"] as string,
				City = reader["CITY"] as string,
				Region = reader["REGION"] as string,
				PostalCode = reader["POSTAL_CODE"] as string,
				Country = reader["COUNTRY"] as string,
				Phone = reader["PHONE"] as string,
				Fax = reader["FAX"] as string,
			};
		}
	}
}
```
## ICONS
Fugue Icons are created by Yusuke Kamiyamane
    <http://p.yusukekamiyamane.com/> and licensed under Creative Commons Attribution 3.0 <http://creativecommons.org/licenses/by/3.0/> license. 

