
// Very basic card object for storing card data between controller and handler.
// This version only stores data of the element's id. 0=water, 1=fire, 2=leaf.

public class Card
{
    public int elementID;

    // Basic constructor for the card.
    public Card(int elementID)
    {
        this.elementID = elementID;
    }
}
