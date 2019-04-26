# Train Ticket Machine
## Solution
Solution is based on a [Trie](https://en.wikipedia.org/wiki/Trie) structure. As a summary, this implies to build a tree, by decomposing words in their chars, so each node is a char and childnodes are next possible characters.  
In order to mark words that may be contained in longer ones (e.g.: london and londonderry), a character '\0' is added when inserting the word, so it generates a new leaf. It is removed when retrieving matching words.  

In general, searching is O(m), with m the length of the search string. 
The next characters available are obtained by simply returning the keys of the children nodes.
The matching words are obtained by recursively traversing the tree from the last node obtained by searching, accumulating the results with the search string.  

## Assumptions
- No limited alphabet is expected in the solution. 
- The whole Trie structure is kept in memory and it is assumed there is enough memory for the structure.
- Search string and stations names are case insensitive and trimmed.