csprojcli NodeValue.Set -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n PackageCertificateKeyFile -v hogehoge.pfx -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n PackageCertificateThumbprint -v 1234567890ABCDEF -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n GenerateAppInstallerFile -v False -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppxAutoIncrementPackageRevision -v True -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppxSymbolPackageEnabled -v False -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppxBundle -v Always -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppxBundlePlatforms -v x86 -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppInstallerUpdateFrequency -v 1 -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppInstallerCheckForUpdateFrequency -v OnApplicationRun -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Node.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g PropertyGroup -n AppxSymbolPackageEnabled -v False -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Attribute.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g ItemGroup -n None -a Include -v hogehoge.pfx -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
csprojcli Attribute.Insert -p SimpleNewCsProjUtf8_CRLF.csproj -g ItemGroup -n None -a Include -v Package.StoreAssociation.xml -dry false -output result_SimpleNewCsProjUtf8_CRLF.csproj -allowoverwrite true
