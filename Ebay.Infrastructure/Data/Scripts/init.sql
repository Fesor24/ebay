CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;
DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'prd') THEN
        CREATE SCHEMA prd;
    END IF;
END $EF$;

CREATE TABLE prd.category (
    id uuid NOT NULL,
    name character varying(1000) NOT NULL,
    CONSTRAINT pk_category PRIMARY KEY (id)
);

CREATE TABLE prd.products (
    id uuid NOT NULL,
    name text NOT NULL,
    description text NOT NULL,
    status integer NOT NULL,
    tags text[] NOT NULL,
    condition integer NOT NULL,
    money_amount numeric NOT NULL,
    money_currency text NOT NULL,
    location text NOT NULL,
    is_deleted boolean NOT NULL,
    category_id uuid NOT NULL,
    created_at_utc timestamp with time zone NOT NULL,
    updated_at_utc timestamp with time zone,
    CONSTRAINT pk_products PRIMARY KEY (id),
    CONSTRAINT fk_products_category_category_id FOREIGN KEY (category_id) REFERENCES prd.category (id) ON DELETE CASCADE
);

CREATE INDEX ix_products_category_id ON prd.products (category_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20261015100031__init_10_15_2026_', '10.0.13');

COMMIT;

