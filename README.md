# WebApplicationOrders

SQL scripts to create DB and Tables:
Orders_DB_Create.sql
Orders_DB_DataInsert.sql

1.	Создаем Базу Orders_DB на сервере MS SQL, используя SQL скрипт:

2.	Создаем Таблицы и наполняем данными

3.	Возможности API:

Получение списка товаров - GET api/Goods
Добавление заказа на товар для клиента - POST api/Orders
Получение списка заказов клиента - GET api/orders/{ClientNumber}
Получение перечня детализации заказа - GET api/orders/{Number}/goods
Редактирование заказа - PUT api/Orders?Number={Number}

4.	Для VIP клиентов предоставляется скидка, равная кол-ву заказов, но не более 50%.
