# Advanced-Backend-2026

## Содержание
* [Диаграммы](#диаграммы)
* [Ролевая модель](#ролевая-модель)
* [Примечания](#обратите-внимание)
* [Спецификация аутентификации](#спецификация-схемы-аутентификации)
* [Спецификация Web API](#спецификация-web-api)
    * [Функционал Абитуриента](#функционал-клиента-абитуриента)
    * [Функционал Менеджера](#функционал-менеджера)
    * [Функционал Главного менеджера](#функционал-главного-менеджера)
    * [Функционал Администратора](#функционал-администратора)

## Диаграммы:

![](/Artifacts/C4L1.drawio.svg)

![](/Artifacts/C4L2.drawio.svg)

![](/Artifacts/ActivityRegister.drawio.svg)

![](/Artifacts/ActivityAddProgram.drawio.svg)

![](/Artifacts/МПО%20(1).drawio.svg)

## Ролевая модель:
* Абитуриент - человек, подающий заявление на поступление в учебное заведение.
* Менеджер - сотрудник факультета, основной задачей которого является работа с абитуриентами и их документами.
* Главный менеджер - помимо функционала актера с ролью “Менеджер” имеют возможность управлять течением приёмной кампании.
* Администратор - основной задачей является управление и обслуживание системы. Кроме своих возможностей, он обладает всеми системными функциями пользователей с другими ролями.

### Обратите внимание!
* Иерархия (Админ > Гл.Менеджер > Менеджер > Абитуриент) строго соблюдается. Нижестоящие не имеют доступа ко всем ресурсам вышестоящих.
* Менеджер может просматривать данные всех абитуриентов, но редактировать только тех абитуриентов, для которых он назначен менеджером.
* Главный менеджер и администратор могут просматривать и редактировать данные всех абитуриентов.  
* Количество выбранных программ не может быть больше, чем N (N - конфигурируется в приложении).
Программы должны относиться к одной ступени обучения. Например:
абитуриент “A” не может одновременно выбрать программу с уровнем “Магистратура” и “Аспирантура”;
абитуриент “B” может выбрать программу с уровнем “Специалитет”, если у него  уже выбрана программа с уровнем “Бакалавриат”.
* Если у абитуриента добавлен документ об образовании, уровень выбранной программы должен быть либо аналогичен уровню документа об образовании, либо входить в список доступных для обучения


## Спецификация схемы аутентификации:
### Схема: Access-Refresh tokens

### Access token info:
* Type: JWT
* LifeTime: 900s
* Claims: UserId, UserRole

### Refresh Token info:
* Lifetime: 604 800s (10080min | 168h | 7 days)


## Спецификация Web API:

### Обозначения:
#### Теги:
[ <kbd>no-auth</kbd> ] - эндпоинт не требует аутентификации

[ <kbd>email-notify</kbd> ] - эндпоинт, в процессе (или по окончании) выполнения может отправлять письма на почту.

[Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> <kbd>Manager</kbd> ] - доступ к эндпоинту ограничен на основании роли пользователя в системе. <kbd>Все</kbd> - означает, что любой пользователь, прошедшйи аутентификацию/авторизацию, имеет доступ к ресурсу. Тегом не учитываются ограничения бизнес логики и доступа к чужим ресурсам.


## API Спецификация сервисов:
- [Enrolly.Accounts (сервис аутентификации и управления аккаунтами)](Enrolly/src/Services/Enrolly.Accounts/Enrolly.Accounts.README.md)

- [Enrolly.Documents (сервис управления документами и их сканами: паспорта, документы об образовании)](Enrolly/src/Services/Enrolly.Documents/Enrolly.Documents.README.md)

- [Enrolly.Admissions (сервис, отвечающий за приведение приемной кампании: создание заявок, выбор программ, факультетов)](Enrolly/src/Services/Enrolly.Admissions/Enrolly.Admissions.README.md)

- [Enrolly.EduDictionary (сервис справочник, хранящий актуальную информацию в всех факультетах, программах, доступных документах и уровнях образования)](Enrolly/src/Services/Enrolly.EduDictionary/Enrolly.EduDictionary.README.md)

- [Enrolly.ApiGateway (wip)]()

## Функционал клиента (абитуриента)

### 1. Возможность регистрации в системе:
<details>
   <summary><code>POST api/v1/auth/register</code></summary>

>   <br>
> [ <kbd>no-auth</kbd> ]
>
> [Доступ: <kbd>Все</kbd> ]
>
>### Тело запроса: 
>```json
>{
>  "fullname": "Ivanov Ivan Ivanovich",
>  "email": "admin@example.com",
>  "password": "Admin123!",
>  "dateOfBirth":"2001-01-01",
>  "gender": "Male",
>  "citizenship": "Russian",
>  "phoneNumber": "+7 999 876 43 21"
>}
>```
>### Тело ответа: 
>```json
>{
>  "access_token": "eyJhciOiJIzI1NiInR5cCI6Ikp...",
>  "refresh_token": "wIiwieyJzdWIiOiIxMjMY3ODkb...",
>  "expires_in": 900
>}
>```
>#### Ожидаемые возвращаемые статус-коды: 201 Created, 400 Bad Request, 409 Conflict
</details>

### 2. Возможность аутентификации/авторизации в системе:
<details>
   <summary><code>POST api/v1/auth/login</code></summary>

>   <br>
> [ <kbd>no-auth</kbd> ]
>
> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "email": "admin@example.com",
>   "password": "Admin123!"
> }
> ```
> ### Тело ответа:
> ```json
> {
>   "access_token": "eyJhciOiJIzI1NiInR5cCI6Ikp...",
>   "refresh_token": "wIiwieyJzdWIiOiIxMjMY3ODkb...",
>   "expires_in": 900
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized
</details>

<details>
   <summary><code>POST api/v1/auth/refresh</code></summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело ответа:
> ```json
> {
>   "access_token": "eyJhciOiJIzI1NiInR5cCI6Ikp...",
>   "refresh_token": "wIiwieyJzdWIiOiIxMjMY3ODkb...",
>   "expires_in": 900
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized
</details>

<details>
   <summary><code>POST api/v1/auth/logout</code></summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 401 Unauthorized
</details>

### 3. Возможность сменить данные для входа (email, пароль)

<details>
   <summary><code>PATCH api/v1/auth/credentials/password</code></summary>

>   <br>
>
> [Доступ: <kbd>Все</kbd> ]
> 
> ### Тело запроса:
> ```json
> {
>   "old_password": "Admin123!",
>   "new_password": "Admin12345!",
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized
</details>

<details>
   <summary><code>PATCH api/v1/auth/credentials/email</code></summary>

>   <br>
>
> [Доступ: <kbd>Все</kbd> ]
> ### Тело запроса:
> ```json
> {
>   "email": "admin@example.com",
>   "password": "Admin123!"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 409 Conflict
</details>

### 4. Просмотр личного профиля 
<details>
   <summary>
      <code>GET api/v1/users/{id}/profile</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>GET api/v1/users/me/profile</code> – id автоматически возьмётся из токена
   </summary>

>   <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело ответа:
> ```json
> {
>   "id": "30dd879c-ee2f-11...",
>   "fullname": "Ivanov Ivan Ivanovich",
>   "email": "ivan.ivanov@example.com",
>   "dateOfBirth": "2001-01-01",
>   "gender": "Male",
>   "citizenship": "Russian",
>   "phoneNumber": "+7 999 876 43 21"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 403 Forbidden, 404 Not Found 
</details>

### 5. Обновление личных данных (ФИО, email, номер телефона, дата рождения, пол, гражданство)
<details>
   <summary>
      <code>PATCH api/v1/users/{id}</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>PATCH api/v1/users/me</code> – id автоматически возьмётся из токена
   </summary>

>   <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело запроса: 
> ```json
> {
>   "fullname": "Ivanov Ivan Ivanovich",
>   "email": "ivan.ivanov@example.com",
>   "dateOfBirth": "2001-01-01",
>   "gender": "Male",
>   "citizenship": "Russian",
>   "phoneNumber": "+7 999 876 43 21"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 6. Просмотр документов (отдельно каждого вида)
<details>
   <summary>
      <code>GET api/v1/users/{id}/documents</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>GET api/v1/users/me/documents</code> – id автоматически возьмётся из токена
   </summary>

>   <br>
> 
> [Доступ: <kbd>Все</kbd> ]
>  ### Query – параметры:
> `type (enum, допустимые значения: "Passport", "Diploma")`
> – фильтрация по типу документа
>   
> ### Тело ответа:
> ```json
> [
>   {
>     "type": "Passport",
>     "id":"",
>     "series": "",
>     "number": "",
>     "placeOfBirth": "",
>     "issuedWhen": "",
>     "issuedBy": "",
>     "url": ""
>   },
>   {
>     "type": "Diploma",
>     "id":"",
>     "name": "",
>     "diplomaType": "",
>     "url": ""
>   }
> ]
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 403 Forbidden
</details>

### 7. Возможность скачать скан документа 
<details>
   <summary>
      <code>GET api/v1/users/{id}/documents/{docId}/file</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>GET api/v1/users/me/documents/{docId}/file</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело ответа:
> ```json
> {
>   "url": "string",
>   "expiresIn": 900
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 8. Возможность редактировать данные документа
<details>
   <summary>
      <code>PATCH api/v1/users/{id}/documents/{docId}</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>PATCH api/v1/users/me/documents/{docId}</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело запроса:
> ```json
> {
>    {
>     "type": "Passport",
>     "series": "",
>     "number": "",
>     "placeOfBirth": "",
>     "issuedWhen": "",
>     "issuedBy": ""
>   },
>   {
>     "type": "Diploma",
>     "name": "",
>     "diplomaType": ""
>   } 
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 9. Возможность удалить скан документа
<details>
   <summary>
      <code>DELETE api/v1/users/{id}/documents/{docId}/file</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>DELETE api/v1/users/me/documents/{docId}/file</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 10. Возможность загрузить новый скан документа
<details>
   <summary>
      <code>POST api/v1/users/{id}/documents/{docId}/file</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>POST api/v1/users/me/documents/{docId}/file</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело запроса:
> ```
> (Content-Type: multipart/form-data)
> your_file.pdf
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 11. Возможность посмотреть список программ с возможностью пагинации и следующими фильтрациями:
<details>
   <summary>
      <code>GET api/v1/programs</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> 
>  ### Query – параметры:
> `page (int, default: 1)`
> – номер страницы
> 
> `size (int, default: 10, max: 100)`
> – размер страницы
> 
> `faculty (List<string>, опционально)`
> – фильтрация по факультету
> 
> `level (int, опционально)`
>  – фильтрация по уровню образования
>  
> `mode (string, опционально)`
>  – фильтрация по форме обучения
>  
> `language (string, опционально)`
>  – фильтрация по языку обучения
>  
> `program (string, опционально)`
>  – поиск по названию/коду программы (по части).
> 
> ### Тело ответа:
> ```json
> {
>   "programs": [
>     {
>       "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
>       "createTime": "2026-03-01T19:36:08.230Z",
>       "name": "string",
>       "code": "string",
>       "language": "string",
>       "educationForm": "string",
>       "faculty": {
>         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
>         "createTime": "2026-03-01T19:36:08.230Z",
>         "name": "string"
>       },
>       "educationLevel": {
>         "id": 0,
>         "name": "string"
>       }
>     }
>   ],
>   "pagination": {
>     "size": 0,
>     "count": 0,
>     "current": 0
>   }
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized 
</details>


### 12. Возможность добавить программу в список выбранных программ для обучения
<details>
   <summary>
      <code>POST api/v1/users/{id}/programs</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>POST api/v1/users/me/programs</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело запроса:
> ```json
> {
>   "programId": "3fa85f64-5717-45..."
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 201 Created, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 13. Возможность изменить приоритет программы
<details>
   <summary>
      <code>PATCH api/v1/users/{id}/programs/{programId}</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>PATCH api/v1/users/me/programs/{programId}</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело запроса:
> ```json
> {
>   "priority": "int"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 14. Возможность удалить программу из выбранного списка
<details>
   <summary>
      <code>DELETE api/v1/users/{id}/programs/{programId}</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>DELETE api/v1/users/me/programs/{programId}</code>  – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden
</details>

### 15. Возможность посмотреть список выбранных программ
<details>
   <summary>
      <code>GET api/v1/users/{id}/programs</code>
      <br>
      &nbsp;&nbsp;&nbsp;&nbsp;<code>GET api/v1/users/me/programs</code> – id автоматически возьмётся из токена
   </summary>

> <br>
> 
> [Доступ: <kbd>Все</kbd> ]
> ### Тело ответа:
> ```json
> [
>   {
>     "priority": "int",
>     "program": {
>         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
>         "createTime": "2026-03-06T20:34:51.096Z",
>         "name": "string",
>         "code": "string",
>         "language": "string",
>         "educationForm": "string",
>         "faculty": {
>           "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
>           "createTime": "2026-03-06T20:34:51.096Z",
>           "name": "string"
>         },
>         "educationLevel": {
>           "id": 0,
>           "name": "string"
>       }
>     }
>   }
> ]
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 403 Forbidden
</details>

## Функционал “Менеджера” 

### 1. Взять поступление абитуриента.
<details>
   <summary>
      <code>POST api/v1/admissions/{id}/manager</code>
   </summary>

> <br>
> 
> [ <kbd>email-notify</kbd> ]
> 
> [Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> <kbd>Manager</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 2. Отказаться от поступления абитуриента (вернуть его в общий пул заявок).
<details>
   <summary>
      <code>DELETE api/v1/admissions/{id}/manager</code>
   </summary>

> <br>
> 
> [ <kbd>email-notify</kbd> ]
> 
> [Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> <kbd>Manager</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 3. Просмотреть заявки абитуриентов с пагинацией  и следующими фильтрациями и сортировками:
<details>
   <summary>
      <code>GET api/v1/admissions</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> <kbd>Manager</kbd> ]
> 
> ### Query – параметры:
> `page (int, default: 1)`
> – номер страницы
> 
> `size (int, default: 10, max: 100)`
> – размер страницы
> 
> `name (string, опционально)`
> – поиск по части имени
> 
> `program (string, опционально)`
> – фильтр по программе
> 
> `faculty (string, опционально)`
> – фильтрация по факультету - multiselect (у абитуриента должна быть выбрана хотя бы одна программа данного факультета)
> 
> `status (enum, опционально)`
> – Фильтрация по статусу поступления
> 
> `isManaged (bool, опционально)`
> – Отображение только тех абитуриентов, у которых еще не назначен менеджер
> 
> `managerId (guid, опционально)`
> – Отображение абитуриентов, привязанных к данному менеджеру
> 
> `dateSort (string, опционально)`
> – Сортировка по дате внесения последних изменений (по убыванию, по возрастанию)
> 
> ### Тело ответа:
> ```json
> {
>   "admissions":
>     [
>       {
>         "userId":"guid",
>         "admissionId":"guid",
>         "status": "enum",
>         "programs" : [
>                 {
>             "priority": "int",
>             "program": {
>                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
>                 "createTime": "2026-03-06T20:34:51.096Z",
>                 "name": "string",
>                 "code": "string",
>                 "language": "string",
>                 "educationForm": "string",
>                 "faculty": {
>                   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
>                   "createTime": "2026-03-06T20:34:51.096Z",
>                   "name": "string"
>                 },
>                 "educationLevel": {
>                   "id": 0,
>                   "name": "string"
>                 }
>               }
>           }
>         ]
>       }
>     ],
>   "pagination": {
>     "size": 0,
>     "count": 0,
>     "current": 0
>   }
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized, 403 Forbidden
</details>


### 4. Изменить статус поступления
<details>
   <summary>
      <code>PATCH api/v1/admissions/{id}</code>
   </summary>

> <br>
> 
> [ <kbd>email-notify</kbd> ]
> 
> [Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> <kbd>Manager</kbd> ]
> ### Тело запроса:
> ```json
> {
>   "status": "enum"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

## Функционал “Главного менеджера” 

### 1. Возможность посмотреть список менеджеров, главных менеджеров
<details>
   <summary>
      <code>GET api/v1/managers</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> ]
> 
> ### Query – параметры:
> `position (enum, Допустимые значения: "Manager", "GeneralManager")`
> – должность менеджера
> ### Тело ответа:
> ```json
> [
>   {
>     "id": "30dd879c-ee2f-11...",
>     "fullname": "Ivanov Ivan Ivanovich",
>     "email": "ivan.ivanov@example.com",
>     "dateOfBirth": "2001-01-01",
>     "gender": "Male",
>     "citizenship": "Russian",
>     "phoneNumber": "+7 999 876 43 21",
>     "role": "enum"
>   }
> ]
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized, 403 Forbidden
</details>

### 2. Возможность назначить менеджера на поступление, если оно свободно
<details>
   <summary>
      <code>PATCH api/v1/admissions/{id}/manager</code>
   </summary>

> <br>
> 
> [ <kbd>email-notify</kbd> ]
> 
> [Доступ: <kbd>Admin</kbd> <kbd>Gen.Manager</kbd> ]
> ### Тело запроса:
> ```json
> {
>   "managerId": "23f45e89-8b5a-5c55..."
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

## Функционал “Администратора” 
### 1. Возможность импорта справочников: факультет, программа, уровень образования
<details>
   <summary>
      <code>GET api/v1/admin/references</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Admin</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 202 Accepted, 400 Bad Request, 401 Unauthorized, 403 Forbidden
</details>

### 2. Возможность посмотреть статус импорта справочников
<details>
   <summary>
      <code>GET api/v1/admin/references/status</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Admin</kbd> ]
>
> ```json
> {
>  "importId": "3fa25sdv4-57...",
>  "lastImport": "2026-03-14T09:00:00Z",
>  "status": "Completed",
>  "details": [
>    {"dictionary": "Faculty",  "count": 1,"status": "Success"},
>    {"dictionary": "Programs", "count": 1, "status": "Success"},
>    {"dictionary": "EduLevel", "count": 1, "status": "Success"}
>  ]
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized, 403 Forbidden
</details>

### 3. Возможность создать нового менеджера, главного менеджера
<details>
   <summary>
      <code>POST api/v1/managers/{id}</code>
   </summary>

> <br>
> 
> [ <kbd>email-notify</kbd> ]
> 
> [Доступ: <kbd>Admin</kbd> ]
> ### Для создания менеджера ему требуется сначала зарегистрироваться как обычный пользователь, в последствии чего администратор системы повысит его роль до "менеджер". Для главного менеджера система схожая, такое же повышение с "менеджера" до "главного менеджера".
>
> #### Ожидаемые возвращаемые статус-коды: 201 Created, 400 Bad Request, 401 Unauthorized, 403 Forbidden
</details>

### 4. Возможность отредактировать данные менеджера 
<details>
   <summary>
      <code>PATCH api/v1/managers/{id}</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Admin</kbd> ]
> ### Тело запроса: 
> ```json
> {
>   "email": "admin@example.com",
>   "fullname": "Ivanov Ivan Ivanovich",
>   "email": "ivan.ivanov@example.com",
>   "dateOfBirth": "2001-01-01",
>   "gender": "Male",
>   "citizenship": "Russian",
>   "phoneNumber": "+7 999 876 43 21"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>

### 5. Возможность удалить менеджера, главного менеджера
<details>
   <summary>
      <code>DELETE api/v1/managers/{id}</code>
   </summary>

> <br>
> 
> [Доступ: <kbd>Admin</kbd> ]
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
</details>