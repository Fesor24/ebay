START TRANSACTION;
ALTER TABLE prd.products ADD user_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE INDEX ix_products_user_id ON prd.products (user_id);

ALTER TABLE prd.products ADD CONSTRAINT fk_products_user_user_id FOREIGN KEY (user_id) REFERENCES usr.users (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20261116132838___user_product_rel_', '10.0.13');

COMMIT;

