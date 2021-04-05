# Инструкция по развертыванию локальной инфраструктуры

## Развертывание БД в *docker*-контейнере

- Для запуска контейнера с БД выполнить в корневом каталоге:

	`docker-compose pull`

	`docker-compose up -d`
- Создает **persisted volumes** персистентный сервис для работы с БД (PostgreSQL \ MS SQL Server), который биндится на дефолтные порты:
	PostgreSQL
		- 5432:5432
  
	MSSQL 
		- 1433:1433
  
- Отключение: 
  
  `docker-compose down` 
  
  (порты свободны, но **persisted volumes** остаются).
  
# Инструкция по подключению Yandex Object Storage

- Полная инструкция как начать работать с Yandex Object Storage - https://cloud.yandex.ru/docs/storage/quickstart
  
### Установка AWS CLI 
 
- Перейти по ссылке - https://aws.amazon.com/ru/cli/ загрузить и установить интерфейс командной строки для вашей операционной системы.

### Настройка

- Для настройки AWS CLI используйте команду: `aws configure`.  Команда запросит значения для следующих параметров:
    1. AWS Access Key ID — введите идентификатор ключа, который вы получили при генерации статического ключа.
    2. AWS Secret Access Key — введите секретный ключ, который вы получили при генерации статического ключа.
    3. Default region name — введите значение ru-central1.
    
- Полная инструкция по AWS Command Line Interface (AWS CLI) - https://cloud.yandex.ru/docs/storage/tools/aws-cli