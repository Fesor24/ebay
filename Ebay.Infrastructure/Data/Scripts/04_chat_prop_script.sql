START TRANSACTION;
DROP INDEX prd.ix_chats_recipient_id;

DROP INDEX prd.ix_chats_sender_id;

CREATE INDEX ix_chats_recipient_id ON prd.chats (recipient_id);

CREATE INDEX ix_chats_sender_id ON prd.chats (sender_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20261117185736___chat_prop_correct', '10.0.13');

COMMIT;

