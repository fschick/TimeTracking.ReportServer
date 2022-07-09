const {resolve} = require('path');
const {readdir, readFile, writeFile} = require('fs').promises;

const p = `
  <Import Project="../../../Build/targets/build_core.props" />
  <Import Project="../../../Build/targets/build_version.props" />
  <Import Project="../../../Build/targets/build_nuget.props" />

  <ItemGroup>
    <None Include="../../../FS.TimeTracking.ReportServer.png" Pack="true" PackagePath="Schick.TimeTracking.ReportServer.png"/>
    <None Include="../../README.md" Pack="true" PackagePath="README.md" />
  </ItemGroup>
`;

const fixes = [
	// Sample:
	// {search: /cc/g, replace: 'ee'},    
	
    // DateTime is already imported by adjusted template.
	{search: /<PropertyGroup>.*<\/PropertyGroup>/s, replace: ''},
	{search: /<Project Sdk="Microsoft.NET.Sdk">/g, replace: `<Project Sdk="Microsoft.NET.Sdk">${p}`},
];

console.log('Replace version information ...');
replaceInFile('src/FS.TimeTracking.Report.Client/FS.TimeTracking.Report.Client.csproj', fixes).then(() => console.log('finished'));
// replaceInDirectory(directory, fixes).then(() => console.log('finished'));

async function replaceInDirectory(directory, replacements) {
    for await (const file of getFiles(directory))
        await replaceInFile(file, replacements);
}

async function* getFiles(directory) {
    const dirEntries = await readdir(directory, {withFileTypes: true});
    for (const entry of dirEntries) {
        const entryName = resolve(directory, entry.name);
        if (entry.isDirectory())
            yield* getFiles(entryName);
        else
            yield entryName;
    }
}

async function replaceInFile(file, replacements) {
	console.log('Replace for file:', file);
    const fileContent = await readFile(file, 'utf8');

    let replacedContent = fileContent;
    for (const replacement of replacements)
        replacedContent = replacedContent.replace(replacement.search, replacement.replace);

    if (replacedContent !== fileContent)
        await writeFile(file, replacedContent, 'utf8');
}