# Extensions for Tile World Creator 3

This is a Unity UPM repository containing extensions to Tile World Creator 3 by Giant Grey
(formerly DoorFortyFour)(http://www.tileworldcreator.com/).

The repository contains ONLY my custom actions, Tile World Creator 3 (TWC3) itself
is **NOT** included; it may be purchased from Unity's Asset Store:
https://assetstore.unity.com/packages/tools/level-design/tileworldcreator-3-199383.

> :warning: Please note that you do not need this repository if you are using Tile
> World Creator version 3.4.0 or later because these extensions have been incorporated
> into the base software.  (Thank you Marc.)
>
> This repository will remain for posterity and for those who are unable to upgrade to
> 3.4.0.

Please note that:

1. I am not the author of TWC3 nor am I affiliated with Giant Grey in any way, I'm just a customer.
2. This repository will be of no use to you without TWC3.
3. Minimum Unity version is the same as for TWC3.


## Licence

MIT.  See the [LICENSE](LICENSE).


## Installing

1. Ensure TWC3 is already installed in your project; you'll get compiler errors from the next step
   if it isn't.
2. Install this UPM package, either:

   1. Use Unity's built-in package manager to install from this repo's URL (or your own fork), or
   2. Clone it directly into your project's `Packages` directory.

   (Option (i) installs the package read-only; option (ii) allows modification.)

Nothing else is needed - TWC3 will auto-detect the extensions.


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

![Image: Example using Overlap With](./Docs%20Images/Overlap%20With.png)

The motivation for this modifier is to make `Overlap` easier to use in the middle of an action stack and
thereby reduce the number of blueprint layers.

### Select By Rule

This modifier is similar the built-in `Select` modifir with a type of `Rule` but instead of having just
two options for testing the corresponding map cell `Select By Rule` has three.  The rule visualisation
is also changed.  The three options are:

* The map cell must be occupied - represented by a green "Y" in a green box.
* The map cell must be unoccupied - represented by a red "N" in a red box.
* Ignore the map cell - we don't care whether it's occupied or unoccupied - represented by an empty
  grey box.

A new rule defaults to all cells being "N"; clicking a cell cycles from "N" -> "Y" -> Blank -> "N"
... etc. (i.e. unoccupied -> occupied -> don't care -> unoccupied ... etc.).

This version also adds the ability to try matching the rule when it is rotated by 90, 180, and 270
degrees.  Each rotation is selectable individually or in any combination.

![Image: Example Select By Rule that selects all cells having at least one outside edge.](./Docs%20Images/Select%20By%20Rule.png)

Image: Example Select By Rule that selects all cells having at least one outside edge.

The motivation for this is to (hopefully) simplify rule creation by removing the need to create 
multiple rules to cover cases where the only difference is the presence or absence of an occupied
map cell in a particular position, or the rule's orientation relative to the map.

### Subtract From

This modifier is similar to the built-in `Substract` modifier but performs the calculation in reverse,
i.e. the built-in `Subtract` subtracts another named blueprint layer from the current map while 
`Subtract From` subtracts the current blueprint layer from another named layer.

![Image: Example Select By Rule that selects all cells having at least one outside edge.](./Docs%20Images/Subtract%20From.png)

The motivation for this modifier is subtraction (unlike addition) is not commutative (i.e., in general,
A - B != B - A) so while `Subtract` provides A - B, `Subtract From` provides B - A (where A is the current
map and B is the other named blueprint layer).

## Notes

All trademarks acknowledged.
