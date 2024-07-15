# HumanReadableId    

Creates a human readable IDs like "happy-energetic-dog-runs-quickly". Use the default format of adjective-adjective-animal-verb-adjective or a custom format and length of your choosing.

## Example Usage

[https://dotnetfiddle.net/4KGjWQ](https://dotnetfiddle.net/4KGjWQ)

```csharp
// Create an ID in the default form adjective-adjective-animal-verb-adjective
// That's 3,579,066,620,200,363 possible combinations using the default config
// id == "pale-swift-hornbill-support-saddlebrown"
var id = HumanReadableId.Create(); 

// Create an ID in a custom format and length
// id == "walk-horrible-confess-question"
id = HumanReadableId.Create(WordType.Verb, WordType.Adjective, WordType.Verb, WordType.Verb);

// Create an ID using a custom separator in pascal case
// id == KickJumpyFruitfly
id = HumanReadableId.Create(new Config
{
    SeparationChar = string.Empty,
    LowerCasePassphrase = false
}, WordType.Verb, WordType.Adjective, WordType.Animal);

```