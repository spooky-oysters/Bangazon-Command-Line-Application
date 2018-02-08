select op.id as "Order Number", c.name as "Customer Name", p.name as "Product", p.quantity as "Quantity", p.price as "Price"
from OrderProduct op, Customer c, Product p, Order o
where c.id == o.customerId,
and o.id