use invoicing;

alter table Invoice add CustomerContactId integer;

alter table Company add InvoiceLogoPercentage float;

alter table Invoice add OurReferencePerson varchar(255);
