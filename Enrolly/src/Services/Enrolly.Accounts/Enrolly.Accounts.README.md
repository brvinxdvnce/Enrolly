## Auth

### 1. Регистрация
<details>
   <summary>
      <code>POST api/v1/auth/register</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "userName": "string",
>   "email": "string",
>   "password": "string",
>   "phoneNumber": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request
</details>

---

### 2. Вход в систему
<details>
   <summary>
      <code>POST api/v1/auth/login</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "email": "string",
>   "password": "string"
> }
> ```
>
> ### Тело ответа:
> ```json
> {
>   "userId": "guid",
>   "accessToken": "string",
>   "refreshToken": "string"
> }
> ```
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized
</details>

---

### 3. Обновление токенов (refresh)
<details>
   <summary>
      <code>POST api/v1/auth/refresh</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "accessToken": "string",
>   "refreshToken": "string"
> }
> ```
>
> ### Ответ:
> ```json
> {
>   "accessToken": "string",
>   "refreshToken": "string"
> }
> ```
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 401 Unauthorized, 404 Not Found
</details>

## Учетные данные

### 1. Изменение пароля
<details>
   <summary>
      <code>PATCH api/v1/auth/credentials/password</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "email" : "string",
>   "oldPassword": "string",
>   "newPassword": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request
</details>

---

### 2. Изменение email
<details>
   <summary>
      <code>PATCH api/v1/auth/credentials/email</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "oldEmail": "string",
>   "newEmail": "string",
>   "password": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request
</details>

---

## Работа с абитуриентами

### 1. Получение списка всех абитуриентов
<details>
   <summary>
      <code>GET api/v1/applicants</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok
</details>

---

### 2. Создание профиля абитуриента
<details>
   <summary>
      <code>POST api/v1/applicants/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "fullname": "string",
>   "email": "string",
>   "dateOfBirth": "2024-01-01",
>   "gender": "string",
>   "citizenship": "string",
>   "phoneNumber": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request
</details>

---

### 3. Получение информации об абитуриенте
<details>
   <summary>
      <code>GET api/v1/applicants/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 4. Обновление данных абитуриента
<details>
   <summary>
      <code>PATCH api/v1/applicants/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "fullname": "string",
>   "email": "string",
>   "dateOfBirth": "2024-01-01",
>   "gender": "string",
>   "citizenship": "string",
>   "phoneNumber": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 404 Not Found
</details>

---

### 5. Удаление профиля абитуриента
<details>
   <summary>
      <code>DELETE api/v1/applicants/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 404 Not Found
</details>

---
## Работа с менеджерами

### 1. Получение списка менеджеров с фильтрацией по званию
<details>
   <summary>
      <code>GET api/v1/managers</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Query параметры:
> - `grade` (optional) - грейд менеджера (DefaultManager, GeneralManager)
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok
</details>

---

### 2. Получение информации о менеджере по ID
<details>
   <summary>
      <code>GET api/v1/managers/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 3. Создание профиля менеджера
<details>
   <summary>
      <code>POST api/v1/managers/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "fullname": "string",
>   "email": "string",
>   "phoneNumber": "string",
>   "grade": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request
</details>

---

### 4. Обновление информации о менеджере
<details>
   <summary>
      <code>PATCH api/v1/managers/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Тело запроса:
> ```json
> {
>   "fullname": "string",
>   "email": "string",
>   "phoneNumber": "string",
>   "grade": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 404 Not Found
</details>

---

### 5. Удаление профиля менеджера
<details>
   <summary>
      <code>DELETE api/v1/managers/{id}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 404 Not Found
</details>

---

### 6. Повышение менеджера в звании
<details>
   <summary>
      <code>POST api/v1/managers/{id}/promote</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 404 Not Found
</details>

---

### 7. Понижение менеджера в звании
<details>
   <summary>
      <code>POST api/v1/managers/{id}/demote</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> #### Ожидаемые возвращаемые статус-коды: 204 No Content, 400 Bad Request, 404 Not Found
</details>