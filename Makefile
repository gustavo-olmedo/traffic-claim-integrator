# Replace docker-compose with docker compose (space, not dash)
.PHONY: up
up:
	docker compose -f infra/local/docker-compose.yml up --build

.DEFAULT_GOAL := up

.PHONY: down
down:
	docker compose -f infra/local/docker-compose.yml down

.PHONY: logs
logs:
	docker compose -f infra/local/docker-compose.yml logs -f

.PHONY: restart
restart:
	docker compose -f infra/local/docker-compose.yml down && docker compose -f infra/local/docker-compose.yml up --build

.PHONY: migrate
migrate:
	docker compose exec dotnet-backend dotnet ef database update --project TrafficClaimIntegratorAPI

.PHONY: bash
bash:
	docker compose exec dotnet-backend /bin/bash
