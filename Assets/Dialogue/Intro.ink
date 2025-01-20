-> intro

// Number of times player used "Huh"
VAR huhNum = 0

=== intro ===
Welcome back!
    * [Who are you?]
        That doesn't matter
            ** [What?]
            ** [Huh?]
                ~ huhNum = huhNum + 1
    * [Thanks!]

- This is a game about choice
    * [Sure]
    * [Huh?]
        ~ huhNum++
    
- You will have a few choices at a time
    * [......]
    * [Huh?]
        ~ huhNum++
    
- That's a lot of choices! 
Enough for your tiny brain to process for a while
    * [Excuse me?]
    * [Huh?]
        ~ huhNum++
    
- Your choices will lead to different outcomes 
    * [Great]
    * [Huh?]
        ~ huhNum++
    
- ... or do they? This might be your manifest destiny
    * [What?]
    * [Huh?]
        ~ huhNum++
    
- Ha! That's how life works. 
{huhNum > 4: 
    My little "huh"mingbird, I will see you soon. 
- else: 
    I will see you soon.
} 

-> END
