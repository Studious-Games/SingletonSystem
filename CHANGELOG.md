# Changelog

## [1.1.0] - 2022-07-14

Fixed an issue with non persistent classes.

Added the ability to use a Locator rather than the static field to retrieve the class instance. In the future it would be good to find a way
to remove the need for using a static instance field to define the Singleton.

Changed the namespace from Studios.Singleton to Studios.SingletonSystem, please be aware that this has changed, and remember to make the necessary changes.

## [1.0.6] - 2022-06-07

Fixed the following:

- Documentation would not display when clicked from with the Package Manager
- Fixed the ability to view the Change Log from with the Package Manager
- Fixed viewing the license from within the Package Manager
- Allowed for the Package to be hidden by default.

## [1.0.5] - 2022-06-05

Added the ability to select what scene a persistent singleton can be unloaded, the caveat to this is that it will destroy all data it contains.

## [1.0.0] - 2022-06-05

This is the initial release of Studious Singleton System,

