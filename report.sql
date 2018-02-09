select 
	op.id as "Order Number", 
	c.name as "Customer Name", 
	c.id as "Id", 
	p.name as "Product", 
	p.quantity as "Quantity", 
	p.price as "Price",
	SUM(p.quantity*p.price) as "Total Revenue Per Product"
from 
	OrderProduct op, 
	Customer c, 
	Product p, 
	`Order` o
where c.id = p.CustomerId
and p.id == op.ProductId
and c.id == o.CustomerId
and o.id == op.OrderId
group by "Product"
order by "Order Number";