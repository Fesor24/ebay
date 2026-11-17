START TRANSACTION;
CREATE TABLE prd.chats (
    id uuid NOT NULL,
    sender_id uuid NOT NULL,
    recipient_id uuid NOT NULL,
    product_id uuid NOT NULL,
    message character varying(250) NOT NULL,
    created_at_utc timestamp with time zone NOT NULL,
    updated_at_utc timestamp with time zone,
    CONSTRAINT pk_chats PRIMARY KEY (id),
    CONSTRAINT fk_chats_product_product_id FOREIGN KEY (product_id) REFERENCES prd.products (id) ON DELETE CASCADE,
    CONSTRAINT fk_chats_user_recipient_id FOREIGN KEY (recipient_id) REFERENCES usr.users (id) ON DELETE CASCADE,
    CONSTRAINT fk_chats_user_sender_id FOREIGN KEY (sender_id) REFERENCES usr.users (id) ON DELETE CASCADE
);

CREATE INDEX ix_chats_product_id ON prd.chats (product_id);

CREATE UNIQUE INDEX ix_chats_recipient_id ON prd.chats (recipient_id);

CREATE UNIQUE INDEX ix_chats_sender_id ON prd.chats (sender_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20261117184021___chat_table__', '10.0.13');

COMMIT;

