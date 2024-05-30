# Auto9Slicer

Auto 9 slice sprite generator on Unity.  
(The library used to be called [`OnionRing`](https://github.com/kyubuns/Auto9Slicer/tree/onionring))

<a href="https://www.buymeacoffee.com/kyubuns" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

![output](https://user-images.githubusercontent.com/961165/106372768-5c612480-63b6-11eb-9ff8-04394f6bb70b.gif)

## Additional Features

<img width="422" alt="image" src="https://github.com/baobao/Auto9Slicer/assets/144386/1d0ab5fd-e9b4-48a7-8999-5f891122dc08">

feature | content 
--- | --- 
Allow Keyword | The keywords at the end of the file term are targeted for slicing. If not specified, all images will be sliced.<br />ex) _sl -> ○ hoge_sl.png × hoge.png 
Is Again Slice | Slices an image that has already been Slice 


## How to Install

Please use Unity Package Manager(UPM).

UPM : https://github.com/baobao/Auto9Slicer.git?path=Assets/Auto9Slicer

![Screenshot 2024-05-31 at 8 12 48](https://github.com/baobao/Auto9Slicer/assets/144386/36fc13a0-8856-4f99-bec7-7888fe94d9cd)

## Instructions

- Package Manager `https://github.com/kyubuns/Auto9Slicer.git?path=Assets/Auto9Slicer`
- [AssetStore](https://assetstore.unity.com/packages/tools/gui/auto-9slicer-188442?aid=1100l3pRW&utm_source=aff)
  - If you purchase it from AssetStore, it will be a AssetStore License. For this reason, it is sold for a fee.

## Simple Use

Create Auto9SliceTester. Assets > Create > Auto 9Slice > Tester

<img width="768" alt="Screen Shot 2021-01-31 at 11 25 46" src="https://user-images.githubusercontent.com/961165/106372836-135da000-63b7-11eb-85ad-d5fc9e6ee655.png">

Click `Run` to automatically slice the images in the same directory.

<img width="874" alt="Screen Shot 2021-01-31 at 11 27 20" src="https://user-images.githubusercontent.com/961165/106372854-4e5fd380-63b7-11eb-9b48-25105fc02edf.png">

## From Script

Add it to your EditorScript so that it will be executed at your desired timing.

```csharp
var slicedTexture = Auto9Slicer.Slicer.Slice(texture, SliceOptions.Default);
textureImporter.spriteBorder = slicedTexture.Border.ToVector4();
File.WriteAllBytes(filePath, slicedTexture.Texture.EncodeToPNG());
```

## Requirements

- Requires Unity2019.4 or later

## License

MIT License (see [LICENSE](LICENSE))

## Buy me a coffee

Are you enjoying save time?  
Buy me a coffee if you love my code!  
https://www.buymeacoffee.com/kyubuns

## "I used it for this game!"

I'd be happy to receive reports like "I used it for this game!"  
Please contact me by email, twitter or any other means.  
(This library is MIT licensed, so reporting is NOT mandatory.)  
https://kyubuns.dev/

