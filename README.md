# Extensions for Tile World Creator 3

This is a Unity UPM repository containing extensions to DoorFortyFour's Tile World Creator 3
(http://www.tileworldcreator.com/).

The repository contains ONLY my custom actions, Tile World Creator 3 (TWC3) itself
is **NOT** included.  TWC3 may be purchased from Unity's Asset Store:
https://assetstore.unity.com/packages/tools/level-design/tileworldcreator-3-199383.

Please note that:

1. I am not the author of TWC3 nor am I affiliated with DoorFortyFour in any way, I'm just a customer.
2. This repository will be of no use to you without TWC3.
3. Minimum Unity version is the same as for TWC3.


## Licence

MIT.  See [LICENCE](LICENSE).


## Installing

1. Ensure TWC3 is already installed in your project; you'll get compiler errors from the next step
   if it isn't.
2. Install this UPM package, either:

   1. Use Unity's built-in package manager to install from this repo's URL (or your own fork), or
   2. Clone it directly into your project's `Packages` directory.

   (Option (i) installs the package read-only; option (ii) allows modification.)

Nothing else is needed - TWC3 will auto-detect the extension.


# Custom Actions

## Generators

There are no custom generators.


## Modifiers

### Overlap With

This modifier is similar to the built-in `Overlap` modifier in that it produces a blueprint map
containing only the cells that are common between two blueprint layers (i.e. a boolean AND) except
that `Overlap With` works more like the built-in `Add` and `Subtract` modifiers in that it
overlaps another, named blueprint layer with the current blueprint layer's map as it is when
`Overlap With` is invoked.

The motivation for this modifier is to make `Overlap` easier to use in the middle of an action stack.



## Notes

All trademarks acknowledged.
