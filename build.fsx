#r "./src/packages/FAKE.4.7.2/tools/FakeLib.dll"

open Fake
open System.IO;

//RestorePackages()
 
// Properties
let buildDir = "./build/"
let deployDir = "./deploy/"
 
// version info
let version = environVarOrDefault "PackageVersion" "1.0.0.0"  // or retrieve from CI server
let summary = "Open source, portable library for interacting with Plex Media Server."
let copyright = "Ian Bebbington, 2015"
let tags = "Plex Media Server PMS"
let description = "Open source, portable library for interacting with Plex Media Server."

let portableAssemblies = [ "OneCog.Io.Plex.dll"; "OneCog.Io.Plex.pdb"; ]

let libDir = "lib"
let srcDir = "src"
let portableTarget = "portable-win81+wpa81+net45+uap10.0"
let uapTarget = "uap"
let srcTarget = "src"
 
// Targets
Target "Clean" (fun _ ->
    CleanDirs [ deployDir ]
)
 
Target "Build" (fun _ ->
   !! "./src/**/*.csproj"
     |> MSBuildRelease buildDir "Build"
     |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    !! (buildDir + "*.Test.dll") 
    ++ (buildDir + "*.Tests.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             ExcludeCategory = "Integration"
             OutputFile = buildDir + "TestResults.xml" })
)

Target "Package" (fun _ ->

    // Copy to deployment folder
    CopyWithSubfoldersTo deployDir [ !! "./src/**/*.cs" ]
    portableAssemblies |> List.map(fun a -> buildDir @@ a) |> CopyFiles deployDir  

    // Setup files to include in package
    let portableFiles = portableAssemblies |> List.map(fun a -> (a, Some(Path.Combine(libDir, portableTarget)), None))
    let uapFiles = portableAssemblies |> List.map(fun a -> (a, Some(Path.Combine(libDir, uapTarget)), None))
    let srcFiles = [ (@"src\**\*.*", Some "src", None) ]

    let dependencies = getDependencies "./src/OneCog.Io.Plex/packages.config" |> List.filter (fun (name, version) -> name <> "FAKE")
    
    NuGet (fun p -> 
        {p with
            Authors = [ "Ian Bebbington" ]
            Project = "OneCog.Io.Plex"
            Description = description
            Summary = summary
            Copyright = copyright
            Tags = tags
            OutputPath = deployDir
            WorkingDir = deployDir
            SymbolPackage = NugetSymbolPackage.Nuspec
            Version = version
            Dependencies = dependencies
            Files = portableFiles @ uapFiles @ srcFiles
            Publish = false }) 
            "./OneCog.Io.Plex.nuspec"
)

Target "Run" (fun _ -> 
    trace "FAKE build complete"
)
  
// Dependencies
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Package"
  ==> "Run"
 
// start build
RunTargetOrDefault "Run"