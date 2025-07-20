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
	@if [ ! -d apps/TrafficClaimIntegratorAPI/Migrations ]; then \
		echo "➕ Creating initial EF Core migration..."; \
		cd apps/TrafficClaimIntegratorAPI && dotnet ef migrations add InitialCreate; \
	else \
		echo "✅ Migrations already exist."; \
	fi && \
	cd apps/TrafficClaimIntegratorAPI && dotnet ef database update

.PHONY: migration # make migration name=AddClaimsTable
migration:
	docker compose exec dotnet-backend dotnet ef migrations add $(name)

.PHONY: bash
bash:
	docker compose exec dotnet-backend /bin/bash
