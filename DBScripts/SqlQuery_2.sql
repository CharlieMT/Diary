﻿DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Students')
AND col_name(parent_object_id, parent_column_id) = 'LastName';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Students] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Students] ALTER COLUMN [LastName] [nvarchar](100) NOT NULL
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202207211240408_SetlastNameMaxLenghtAndRequiredInStudentsTable', N'Diary.ApplicationDbContext',  0x1F8B0800000000000400ED5ADB6EE336107D2FD07F10F4D416592BCEBEB481BD8BAC93144637C922CE2EFA16D0D2D8612B51AA4805368A7E591FFA49FD850E75A54849969CDB765B04086C9273E1F0CC903CF4DF7FFE3579BB097CEB1E624E4336B5C7A343DB02E6861E65EBA99D88D5ABEFEDB76FBEFE6A72E6051BEB5331EEB51C87928C4FED3B21A263C7E1EE1D04848F02EAC6210F5762E4868143BCD0393A3CFCC1198F1D401536EAB2ACC975C2040D20FD825F672173211209F12F420F7C9EB763CF22D56A5D920078445C98DAA794C45BDB3AF12941E30BF057B645180B0511E8DAF1470E0B11876CBD88B081F837DB0870DC8AF81C72978FABE17DBD3F3C92DE3B9560A1CA4DB80883810AC7AFF37038BAF85E41B5CB7061C0CE30B0622B679D066D6AFF188749645BBAA5E3991FCB5179404759E047A73886323E4AA50EACB4EFA05C778487FC3BB066892F9218A60C121113FFC0FA902C7DEAFE04DB9BF057605396F8BEEA163A867DB5066CFA108711C4627B0DABDCD9B9675B4E5DCED1054B3145269BC99C89D747B67589C6C9D28772D595592F4418C38FC0202602BC0F44088871D12E43068665CD8EFC5F5842886182D8D605D9BC07B6167753FB0833E29C6EC02B1A72E31F19C57442191127D0E09C66F492DCD375EAAB667E21120F98E0B6750D7E3A80DFD128CB8051DE799B2FF6791C06D7A15F49651DB737245E83C049844DBD8B30895DCDA3895301AA1366B9AAC140CBE5FE3B509BCBE962EC76C1ED9CC65CECC0DCF8F07140A7997E4F5ECAF22C0C820CE4AD96F1632FCBDD864E5C41EFA9A0509A7A17624A1036D8E534777603A36F9AE719BC7F8E1759DC9CE34505E8EBCE35C120B17573D5C93B6FCBDCAF7CD2BA8CCAA3F73FA8F6E4CA06D79E5CEEFFDAA3DBBA46A981806EDEAD06E785AE2659FE02EE7035BB76D187C2594FB236B8F781F309E7A14B5367EA4EE6995E9FD919F3ACCEB4CFEAA65230B07A228C6984C045EB53FB3B235A6D3ACBA4AD74E6C5A7AE716CEB90BF62A7E083004BD659792A9E11EE12CF5C288C89576FC12C8158C294F8782DE09877940933A528736944FC2EBF35A19E99289D2AD5EB3DA710019396BAD6A08FDD72D7308D9736B448ED0ACCC45190D40D301DE96D7068857D0588B2F6F60759EBDEB013BA2F0FB416DF9F016A2D6BD1C7B2528C9F016E599543198112101707AE48AE623AF874293B61D37459C0EB7BBE67F3FC20A7A3482A5F8050F3084F275569AD172A038475F1EA5265282821B843457542325494B9A1A9500267B8521CEF94318DE73F7D1D77ED0BA5DBEAA40D34ECDA09142D45E4F53CAB4FAEC7C48D6DD79C7A67BDEA55B114C7AB05EB987E5B8DEA11C68E1014478032392AA2CBC998AE8211735A28B1C9058922E97D2599B7588B8C1F9BBD5A0C6791824C87E3F20632A9F4B6B484A74BB206AD57DEDE3C48AFADA744902591E7B2991718C31A4B414B9215266BD96E2E5B9178C570F93913C90EFE8D3953C5EE1CA723EF9DE9CC4007B929971293C42771C3197D16FA49C0DA4B7EBB7476E556E5B31653C3C4D11C37AABA11126337AC07B857F84BC40F5F8096CCEDB104AD924FB3080AEDA22A519AFBEBAA78145555D5DA5F53C58BA89AAAD6FE9A54E243D5A5B6F7D7561E635555AD67DB17036E59F28703B7D8C38703B755F269809BDDD955F9ACA5BF06E598A8AAE9383D76E8AA2EEE355D55F33383C3D87AF521A5F5720BD6B6DA49BEEDED7EA232F6C16C886D6190EEA927F7C0C5960B084672C068F19B3FF3697AF429065C104657C0454633D94787E323EDC9EBF3797E7238F7FC3E6F50CF4E945119D2818C93CA7BB37B12BB772436DE793A753EC293C91C2F649BA9FDBBF5C70B056D277F3830AAC663466368D3478507BE553C9A62FD29A250FC4D4036DFAADAF67B6E58D2E1D8D49E1A683776A4CCB135FFF936173BB0AE622C2DC7D621C2EA9120DCCEBC7F71105639F17D0A8BC188F75DBE5270C002F660D277CFE1B109EB9CD9785636D92034F663C8F7E20ADB2E9D4FC5427F41C4F380C57A22B894956D1FAEFB216F18CFC825FF6BC8639358EAC10AB793C2D9717C6A7BCB10D737AB7F2D74660B61DCC51737696FE50A5BD8E42E32B9493F6E4E4D94E61333CDB5302B7C590F5EB98949FDDCB8E401AE36978E0632E049B862F3628A19A8FCC212F39FD375A542FEDE92815BCBBD72CC9CADC2A20A681E1543B4A3C50508E261629EC482AE882BB0DB05CED3E97E227E8243CE822578737695882811386508967EED5708B29474D94F09F1BACF93AB287DC07F8C29A09B14A70057EC5D427DAFF4FBBCE154D4A242D6A8FCB829D752C863E77A5B6A327F61D8A6280F5F595A6F20887C54C6AFD882DCC33EBE7DE4F01ED6C4DD16FC42BB92DD0B510FFBE49492754C029EEBA8E4F12B62D80B366FFE01E9A1F315682C0000 , N'6.2.0-61023')
