# HumanReadableId    

![.NET Core](https://github.com/GFoley83/HumanReadableId/workflows/.NET%20Core/badge.svg?branch=master)

Create human-readable Ids, just like Gfycat. Use the default format of _VerbAdjectiveAnimal_ or a custom format of your choosing.

## Example Usage

[https://dotnetfiddle.net/4KGjWQ](https://dotnetfiddle.net/4KGjWQ)

```csharp
// Create Id in the default form VerbAdjectiveAnimal
var id = GfycatesceIds.Generate(); // id == "ExaltedDifficultAntelope"

// Create classic Gfycat Id in the form AdjectiveAdjectiveAnimal
var id = GfycatesceIds.Generate(GfycatesceIds.GfycatPattern); // id == "CalmFriendlyLion"

// Create Id in a custom form and length
var id = GfycatesceIds.Generate(new[]
    {
        WordType.Verb, WordType.Adjective, WordType.Verb, WordType.Verb, WordType.Animal
    }); // id == "WalkHorribleConfessQuestionZebra"
```