START TRANSACTION;
CREATE TABLE usr.wishlists (
    id uuid NOT NULL,
    user_id uuid NOT NULL,
    product_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    CONSTRAINT pk_wishlists PRIMARY KEY (id),
    CONSTRAINT fk_wishlists_products_product_id FOREIGN KEY (product_id) REFERENCES prd.products (id) ON DELETE CASCADE,
    CONSTRAINT fk_wishlists_users_user_id FOREIGN KEY (user_id) REFERENCES usr.users (id) ON DELETE CASCADE
);

CREATE INDEX ix_wishlists_product_id ON usr.wishlists (product_id);

CREATE INDEX ix_wishlists_user_id ON usr.wishlists (user_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20261119163339___wishlist_ent__', '10.0.13');

COMMIT;

