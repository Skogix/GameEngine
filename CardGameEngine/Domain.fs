module CardGameEngine.Domain

type Suit = Hearts | Diamonds | Spades | Clubs
type CardValue =
  | Ace
  | Two
  | Three
  | Four
  | Five
  | Six
  | Seven
  | Eight
  | Nine
  | Ten
  | Jack
  | Queen
  | King
type Card = Suit * CardValue
type CardPool = Card list
let deck =
  [
    for value in [Ace;Two;Three;Four;Five;Six;Seven;Eight;Nine;Ten;Jack;Queen;King] do
      yield Card(Hearts, value)
      yield Card(Diamonds, value)
      yield Card(Spades, value)
      yield Card(Clubs, value)
  ]
type Deck = CardPool
type Hand = CardPool
type TakeCardFromDeck = Deck -> Card * Deck
type GiveCardToHand = Hand -> Card * Deck -> Hand * Deck
type Deal = Deck -> Hand -> Deck * Hand

let takeCardFromDeck: TakeCardFromDeck = fun deck -> deck.Head, deck.Tail
let giveCardToHand: GiveCardToHand = fun hand (card, deck) -> card::hand, deck
let deal: Deal = fun hand deck -> takeCardFromDeck deck |> giveCardToHand hand
let hand = []
deck
|> deal hand