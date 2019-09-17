# C# Rocks!

Welcome to the Wizkids C# test!

This small test gives you the opportunity to show off a few of your C# coding skills and some logical thinking.

You can create your own solution or use the `Program.cs` sample file (replacing as appropriate, including inside `Main()` if you so desire).

## Tasks

#### 1. Write a method that determines if a string is a palindrome or not.

#### 2. Write a method that can find and replace valid email adresses in a (string) using a regex.
For example, find and replace all valid email adresses in the following text:

```
Christian has the email address christian+123@gmail.com.
Christian's friend, Lars-Ole Jensen, has the email address lars-ole.jensen@gmail.com.
Lars-Ole's daugther Britt studies at Oxford University and has the email adress britt123@oxford.co.uk.
```

##### 2.a In the summary of the method explain your considerations about the regex.

#### 3. Write a method that can generate a list of words based on input word and alphabet.
In spell checking it is assumed that all words are wrong and alternative words are proposed if they fit better in the context. One way to generate alternative words is taking the original word and applying certain operations:

- Deleting a letter.
- Inserting a letter.
- Replacing a letter.
- Swapping two adjacent letters.

If only one operation is performed on the original word the Damerau–Levenshtein distance between the original word and the new alternative word is 1, for example, `herroes [DELETE OPERATION] => heroes`. If two or more operations are performed on the new alternative word(s) the Damerau–Levenshtein distance increases with the amount of operations, for example, `herros [DELETE OPERATION] => heros [INSERT OPERATION] => heroes` yields a Damerau–Levenshtein distance of 2.

The method should generate all possible alternative words based on the 4 operations listed above and maximum Damerau–Levenshtein distance = 1.

##### 3.a How many non-unique alternative words can be generated using the word `test`, alphabet `a-z` (26 letters) and maximum Damerau–Levenshtein distance = 1?

##### 3.b Write a method that can calculate the number of non-unique alternative words based on input word length and alphabet length (assuming maximum Damerau–Levenshtein distance = 1).

#### 4. Add your code in a Git project on GitHub and share the link with your Wizkids contact.

#### 5. (OPTIONAL, but strongly recommended) Share your most awesome C# project (in which you've been a major contributor) with us - Git repository preferred.
