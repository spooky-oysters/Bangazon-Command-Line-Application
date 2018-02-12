/*
    Data to seed production database
*/

DELETE FROM OrderProduct;
DELETE FROM `Order`;
DELETE FROM Product;
DELETE FROM PaymentType;
DELETE FROM Customer;

INSERT INTO Customer VALUES (null,"Customer_1","Street","City","State","PostalCode","PhoneNumber");
INSERT INTO Customer VALUES (null,"Customer_2","Street","City","State","PostalCode","PhoneNumber");
INSERT INTO Customer VALUES (null,"Customer_3","Street","City","State","PostalCode","PhoneNumber");
INSERT INTO Customer VALUES (null,"Customer_4","Street","City","State","PostalCode","PhoneNumber");
INSERT INTO Customer VALUES (null,"Customer_5","Street","City","State","PostalCode","PhoneNumber");
INSERT INTO Customer VALUES (null,"Customer_6","Street","City","State","PostalCode","PhoneNumber");
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "AA Batteries", 5.99, "Test Description_1", 10 from Customer c WHERE c.Name = "Customer_1";
INSERT INTO Product SELECT null, '2017-01-01',c.Id, "Diapers", 19.99, "Test Description_2", 100 from Customer c WHERE c.Name = "Customer_1";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Fuzzy Neck Warmer", 99.99, "Test Description_3", 50 from Customer c WHERE c.Name = "Customer_2";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Scooby Doo Slippers", 199.99, "Test Description_4", 30 from Customer c WHERE c.Name = "Customer_2";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Girl Scout Cookies", 4.99, "Test Description_5", 200 from Customer c WHERE c.Name = "Customer_3";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Rebel Flag Socks", 1.99, "Test Description_6", 2000 from Customer c WHERE c.Name = "Customer_3";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Converse", 5.99, "Test Description_7", 10 from Customer c WHERE c.Name = "Customer_4";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "SQL Lessons", 99.99, "Test Description_8", 1000 from Customer c WHERE c.Name = "Customer_4";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Kiddie Pool Floaties", 119.99, "Test Description_9", 100 from Customer c WHERE c.Name = "Customer_5";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Crock Pot", 29.99, "Test Description_10", 10000 from Customer c WHERE c.Name = "Customer_5";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Kitchen AID", 79.99, "Test Description_11", 100 from Customer c WHERE c.Name = "Customer_6";
INSERT INTO Product SELECT null, '2017-01-01', c.Id, "Preds Tickets", 89.99, "Test Description_12", 100 from Customer c WHERE c.Name = "Customer_6";
INSERT INTO PaymentType SELECT null, c.Id, "AmEx", 8234212999 from Customer c WHERE c.Name = "Customer_1";
INSERT INTO PaymentType SELECT null, c.Id, "Visa", 8234212999 from Customer c WHERE c.Name = "Customer_2";
INSERT INTO PaymentType SELECT null, c.Id, "MasterCard", 8234212999 from Customer c WHERE c.Name = "Customer_3";
INSERT INTO PaymentType SELECT null, c.Id, "Debit Card", 8234212999 from Customer c WHERE c.Name = "Customer_4";
INSERT INTO PaymentType SELECT null, c.Id, "Checking", 8234212999 from Customer c WHERE c.Name = "Customer_5";
INSERT INTO PaymentType SELECT null, c.Id, "Capital One", 8234212999 from Customer c WHERE c.Name = "Customer_6";

INSERT INTO `Order` SELECT null, c.Id, null, null from Customer c, PaymentType p WHERE c.Name = "Customer_1" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/15/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_1" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/25/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_1" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, null, null from Customer c, PaymentType p WHERE c.Name = "Customer_2" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/15/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_2" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/25/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_2" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/1/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_3" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/15/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_3" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/25/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_3" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/1/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_4" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/15/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_4" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/25/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_4" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/1/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_5" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/15/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_5" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/25/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_5" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/1/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_6" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, null, null from Customer c, PaymentType p WHERE c.Name = "Customer_6" AND c.Id = p.customerId LIMIT 1;
INSERT INTO `Order` SELECT null, c.Id, p.Id, "1/25/2018" from Customer c, PaymentType p WHERE c.Name = "Customer_6" AND c.Id = p.customerId LIMIT 1;

-- open order - customer 1
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.PaymentTypeId is null AND p.Name = "Fuzzy Neck Warmer"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.PaymentTypeId is null AND p.Name = "Girl Scout Cookies"  LIMIT 1;

-- closed orders - customer 1
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/15/2018' AND p.Name = "Converse"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_1"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Converse"  LIMIT 1;

-- open order - customer 2
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_2"  
AND o.CustomerId = c.Id AND o.PaymentTypeId is null AND p.Name = "Kitchen AID"  LIMIT 1;

-- closed orders - customer 2
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_2"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/15/2018' AND p.Name = "SQL Lessons"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_2"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Crock Pot"  LIMIT 1;

-- closed orders - customer 3
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_3"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/1/2018' AND p.Name = "SQL Lessons"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_3"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/15/2018' AND p.Name = "Preds Tickets"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_3"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Crock Pot"  LIMIT 1;

-- closed orders - customer 4
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_4"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/1/2018' AND p.Name = "SQL Lessons"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_4"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/15/2018' AND p.Name = "Preds Tickets"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_4"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Crock Pot"  LIMIT 1;

-- closed orders - customer 5
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_5"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/1/2018' AND p.Name = "SQL Lessons"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_5"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/15/2018' AND p.Name = "Preds Tickets"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_5"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Crock Pot"  LIMIT 1;

-- open order - customer 6
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_6"  
AND o.CustomerId = c.Id AND o.PaymentTypeId is null AND p.Name = "Rebel Flag Socks"  LIMIT 1;

-- closed orders - customer 6
INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_6"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/1/2018' AND p.Name = "SQL Lessons"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_6"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/1/2018' AND p.Name = "Crock Pot"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_6"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/15/2018' AND p.Name = "Preds Tickets"  LIMIT 1;

INSERT INTO OrderProduct  SELECT null, o.Id, p.Id  FROM `Order` o, Product p, Customer c  WHERE c.Name = "Customer_6"  
AND o.CustomerId = c.Id AND o.CompletedDate = '1/25/2018' AND p.Name = "Crock Pot"  LIMIT 1;
