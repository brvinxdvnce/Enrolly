## Работа с метаданными документов об образовании

### 1. Добавление информации о документе об образовании
<details>
   <summary>
      <code>POST api/v1/users/{userId}/education-documents</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> ### Тело запроса:
> ```json
> {
>   "documentType": "string",
>   "series": "string",
>   "number": "string",
>   "issueDate": "2024-01-01",
>   "institutionName": "string",
>   "specialty": "string",
>   "qualification": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request
</details>

---

### 2. Получение информации о документе об образовании
<details>
   <summary>
      <code>GET api/v1/users/{userId}/education-documents/{docId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `docId` (guid) - идентификатор документа
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 3. Изменение информации о документе об образовании
<details>
   <summary>
      <code>PATCH api/v1/users/{userId}/education-documents/{docId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> ### Тело запроса:
> ```json
> {
>   "documentType": "string",
>   "series": "string",
>   "number": "string",
>   "issueDate": "2024-01-01",
>   "institutionName": "string",
>   "specialty": "string",
>   "qualification": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 4. Удаление документа об образовании
<details>
   <summary>
      <code>DELETE api/v1/users/{userId}/education-documents/{docId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `docId` (guid) - идентификатор документа
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---
## Работа со сканами документов об образовании

### 1. Добавление скана документа
<details>
   <summary>
      <code>POST api/v1/users/{userId}/education-documents/{docId}/scans</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `docId` (guid) - идентификатор документа
>
> ### Тело запроса:
> - `file` (IFormFile) - файл скана документа (multipart/form-data)
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 2. Получение скана документа
<details>
   <summary>
      <code>GET api/v1/users/{userId}/education-documents/{docId}/scans/{scanId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `docId` (guid) - идентификатор документа
> - `scanId` (guid) - идентификатор скана
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok (возвращает файл), 400 Bad Request, 404 Not Found
</details>

---

### 3. Удаление скана документа
<details>
   <summary>
      <code>DELETE api/v1/users/{userId}/education-documents/{docId}/scans/{scanId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `docId` (guid) - идентификатор документа
> - `scanId` (guid) - идентификатор скана
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

--- 

## Работа с паспортными данными пользователя

### 1. Добавление паспортных данных
<details>
   <summary>
      <code>POST api/v1/users/{userId}/passport</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> ### Тело запроса:
> ```json
> {
>   "series": "string",
>   "number": "string",
>   "issueDate": "2024-01-01",
>   "issuedBy": "string",
>   "departmentCode": "string",
>   "birthPlace": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request
</details>

---

### 2. Получение паспортных данных
<details>
   <summary>
      <code>GET api/v1/users/{userId}/passport</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 3. Изменение паспортных данных
<details>
   <summary>
      <code>PATCH api/v1/users/{userId}/passport</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> ### Тело запроса:
> ```json
> {
>   "series": "string",
>   "number": "string",
>   "issueDate": "2024-01-01",
>   "issuedBy": "string",
>   "departmentCode": "string",
>   "birthPlace": "string"
> }
> ```
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 4. Удаление паспортных данных
<details>
   <summary>
      <code>DELETE api/v1/users/{userId}/passport</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

## Работа со сканами паспорта пользователя

### 1. Добавление скана паспорта
<details>
   <summary>
      <code>POST api/v1/users/{userId}/passport/scans</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
>
> ### Тело запроса:
> - `file` (IFormFile) - файл скана паспорта (multipart/form-data)
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>

---

### 2. Получение скана паспорта
<details>
   <summary>
      <code>GET api/v1/users/{userId}/passport/scans/{scanId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `scanId` (guid) - идентификатор скана
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok (возвращает файл), 400 Bad Request, 404 Not Found
</details>

---

### 3. Удаление скана паспорта
<details>
   <summary>
      <code>DELETE api/v1/users/{userId}/passport/scans/{scanId}</code>
   </summary>

> [Доступ: <kbd>Все</kbd> ]
>
> ### Параметры маршрута:
> - `userId` (guid) - идентификатор пользователя
> - `scanId` (guid) - идентификатор скана
>
> #### Ожидаемые возвращаемые статус-коды: 200 Ok, 400 Bad Request, 404 Not Found
</details>
