# URL Shortener

Простой API для создания коротких URL и сбора статистики по переходам

## Возможности

- Создание коротких ссылок
- Редирект по короткому URL
- Сбор статистики переходов
- REST API
- PostgreSQL в качестве базы данных
- Swagger для демонстрации HTTP-запросов
- Запуск через Docker Compose

## Установка

```bash
git clone https://github.com/ryzij/URL-Shortener.git url-short
cd url-short
```

### Настройка окружения

```bash
cp .env.example .env
```

Пример `.env`:
```
# Укажите свои значения
POSTGRES_USER=app
POSTGRES_PASSWORD=app
POSTGRES_DB=appdb
```

### Настройка `docker-compose.yml`

```bash
cp docker-compose.example.yml docker-compose.yml
```

Пример `docker-compose.yml`:
```yml
services:
  api:
    build:
      context: .
      dockerfile: URL-Shortener/Dockerfile
    ports:
      - "5000:8080"
    depends_on:
      - db
    environment:
      ConnectionStrings__DefaultConnection: Host=db;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}

  db:
    image: postgres:17
    restart: always
    environment:
      POSTGRES_USER: ${POSTGRES_USER}
      POSTGRES_PASSWORD: ${POSTGRES_PASSWORD}
      POSTGRES_DB: ${POSTGRES_DB}
    volumes:
      - pg_data:/var/lib/postgresql/data

volumes:
  pg_data:
```

## Запуск

```bash
docker compose up --build -d
```

После запуска Swagger будет доступен по адресу:
http://localhost:5000/swagger
