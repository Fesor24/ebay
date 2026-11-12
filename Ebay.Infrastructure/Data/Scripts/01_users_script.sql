START TRANSACTION;
DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'usr') THEN
        CREATE SCHEMA usr;
    END IF;
END $EF$;

CREATE TABLE usr.roles (
    id uuid NOT NULL,
    name character varying(50) NOT NULL,
    code character varying(50) NOT NULL,
    CONSTRAINT pk_roles PRIMARY KEY (id)
);

CREATE TABLE usr.users (
    id uuid NOT NULL,
    first_name character varying(125) NOT NULL,
    last_name character varying(125) NOT NULL,
    email character varying(125) NOT NULL,
    username text NOT NULL,
    phone_no character varying(20) NOT NULL,
    address_street character varying(100) NOT NULL,
    address_city character varying(50) NOT NULL,
    address_country character varying(50) NOT NULL,
    address_post_code character varying(10) NOT NULL,
    password text NOT NULL,
    role_id uuid NOT NULL,
    created_at_utc timestamp with time zone NOT NULL,
    updated_at_utc timestamp with time zone,
    CONSTRAINT pk_users PRIMARY KEY (id),
    CONSTRAINT fk_users_roles_role_id FOREIGN KEY (role_id) REFERENCES usr.roles (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX ix_users_email ON usr.users (email);

CREATE INDEX ix_users_role_id ON usr.users (role_id);

CREATE UNIQUE INDEX ix_users_username ON usr.users (username);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20261112094151__users_entities_', '10.0.13');

COMMIT;

