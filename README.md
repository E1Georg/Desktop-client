# Desktop-client
Стандартный desktop-клиент, который в зависимости от действий пользователя может переходить в режим чтения/записи/редактирования/удаления строк(исключений) в базе данных для системы AirLogger(сущность UserException) в реальном масштабе времени.
___
### Руководство пользователя: 
Проект начинается с главной формы(main.png) и может перейти на форму отображения данных - для этого необходимо подключиться к БД. Адрес указывается в файле конфигурациии. На второй форме реализовано только отображение данных в dataGridView. Реализация основных операций по работе с набором данных (CRUD – create, read, update, delete) находится на дополнительных специализированных формах. Проект имеет клиент-серверную архитектуру и является комплексным средством доступа и обработки данных.

![Иллюстрация](https://github.com/E1Georg/Desktop-client/raw/main/Img/main.png)
![Иллюстрация](https://github.com/E1Georg/Desktop-client/raw/main/Img/get%20data.png)
![Иллюстрация](https://github.com/E1Georg/Desktop-client/raw/main/Img/edit.png)
