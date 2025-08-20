**4 mappen**
1. Core
- domeinmodellen


2. Blazor= frontend
- interface

3. WebApi= backend
- EFCore= communicatie tss api en ef core
    - SQLite voor dataopslag
    - interface via swagger

- API
    - in contact met domein via unitOfWork > repo's



4. Tests


**werking**
gebruiker > blazor

blazor> http > api

api: controller methode uitvoeren
- request: ef core>db
- response: db> ef core > api > blazor