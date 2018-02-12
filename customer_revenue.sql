select 
	c.Name as "Customer Name", 
	o.Id as "Order Number", 
	p.Name as "Product Name", 
	p.Quantity as "Quantity",
	p.price as "Price",
	SUM(p.Quantity * p.Price) as "Total Revenue Per Product" 
from 
	Customer c, 
	`Order` o, 
	Product p, 
	OrderProduct op
where c.Id = p.CustomerId
and p.Id = op.ProductId
and c.Id = o.CustomerId
and o.id = op.OrderId
group by p.Name
order by o.Id;