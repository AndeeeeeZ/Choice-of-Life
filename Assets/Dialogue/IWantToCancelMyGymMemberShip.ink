-> Intro

VAR MathScore = 0
VAR PhaseOneDone = 0

=== Intro ===
While many people's New Year Resolution is to go to the gym # narration
Yours is special: # narration
To finally, cancel your gym membership # narration
You are determined, to do it today. # narration
That is your only goal. # narration
-> Intro.Welcome

= Welcome
"Welcome to Space Fitness! How can I help you?"
* ["I want to workout"]
    -- You scanned your ID # narration
    -- AN HOUR LATER... # narration
    -- {~What a great workout! |That was horrible.} # narration
    -- You went back to the front desk # narration
    -> Intro.Welcome
+ ["I want to cancel my gym membership"]
    -> Cancelling
+ ["I want to leave"]
    -> Leaving

=== Cancelling === 
"YOU WANT TO WHAT?"
+ ["I WANT TO CANCEL MY GYM MEMBERSHIP!"]
    -- You were so loud that everyone in the gym stared at you # narration
    -- "no no... NO NO NO!"
    -- "You must be kidding, right?" 
    -> AskingReason
+ ["No, never mind"]
    -> Leaving
    
= AskingReason
~ PhaseOneDone += 1
{ PhaseOneDone > 4: 
"Well, that's a lot of reasons from you"
"But in order for you to cancel your membership"
"You need to talk to our manager..."
"...Who is, unfortunately, not here day"
* ["Well, I will come another day then"]
    -> Leaving
* ["Manager? Are you kidding me?"]
    -> FinalPhase
* ["I @!&% need to cancel my %!@*$ membership TODAY!"]
    -> FinalPhase
}

{ PhaseOneDone <= 4:
"Can you please tell me why you are cancelling your membership?"
}
* ["I'm moving away"]
    -- "Oh, that's not a big deal!"
    -- "Space Fitness have branches all around the country"
    ** ["I still want to cancel my membership"]
        -> AskingReason
    ** ["That's CAP"]
        -> AskingReason
    ** ["Sure! Never mind then"]
        -> Leaving
        
* ["I found a better gym"]
    -- "A BETTER gym?"
    -- "What gym, in the world, is better than the SPACE FITNESS?"
    -- "PLANET FITNESS? Never heard of that place."
    -- "You must be kidding, right?"
        ** ["Of course, Planet Fitness is really mid"]
            -> Leaving
        ** ["I'm not kidding"]
            -> AskingReason
        ** ["Haha yeah. Never mind then"]
            -> Leaving
            
* ["There's too many people"]
    -- "Too many people?"
    -- "Haha, just come at a different time"
    -- "How about 4 a.m. in the morning?"
    -- "You know, early bird get the worm!"
        ** ["That a good point, I will get up early then"]
            -> Leaving
        ** ["That's not funny"]
            -> AskingReason
        ** ["Sure, I love your sense of humor"]
            -> Leaving
        ** ["But I don't eat worms"]
            --- "You idiot! It's just a saying!"
            --- "Fine, I guess people like you wouldn't be up early"
            -> AskingReason
            
* ["I just... can't afford it"]
    -- "We can give you a discount!"
    -- "Your health is always more important than your money right?"
        ** ["Nope. I'm broke."]
            --- "Broke? That's CRAZY"
            --- "Let me test you some math to see if you are actually broke"
            -> MathTest
        ** ["You are right..."]
            -> Leaving
        ** ["Sure, I'll continue working out then!"]
            -> Leaving


=== FinalPhase ===
"Okay, OKAY... Relax my bro"
"Well... I'm just a receptionist..."
"So I don't have the access to cancel your membership"
* [(Hit)]
* [(Slap)]
* [(Punch)]
* [(Smash)]
- "Ouch!"
* ["I had enough of all these BS (Slap)"]
* ["You bastard! (Slap)]
* ["%!@*$" (Slap)]
- "Ahhh!"
- "FINE! I will cancel your membership"
- "You satisfied now?"
* ["Great."]
    -> Success
* ["Thanks"]
    -> Success
* [(Slap)]
    -- "Ouch!"
    -> Success

=== MathTest ===
VAR answer = 0
~ temp i = 0
~ temp n1 = 1
~ temp n2 = 0
~ temp correctChoice = 0
~ temp operation = ""

~ correctChoice = RANDOM(0,3)
~ n1 = RANDOM(1, 7)
~ n2 = RANDOM(8, 15)
~ operation = "{-|+|*}"

{operation == "+": 
    ~ answer = n1 + n2
}
{operation == "-": 
    ~ answer = n1 - n2
}
{operation == "*": 
    ~ answer = n1 * n2
}

~ temp wrongI = answer + RANDOM(1, 10)
~ temp wrongII = answer - RANDOM(1, 10)
~ temp wrongIII = answer * RANDOM(2, 5)
~ temp wrongIV = answer + RANDOM(10, 20)

{MathScore >= 3:
"No way... You correctly answered three questions in a row?!"
"You are actually a mathematician!"
"Yeah I can see that from you"
"Since you are probably, really, broke"
    -> Cancelling.AskingReason
}

"Please answer the following question:"
"What is {n1} {operation} {n2}?"

+ {correctChoice == 0}
    [{answer}]
        -> RightAnswer
+ {correctChoice != 0}
    [{wrongI}]
        -> WrongAnswer
+ {correctChoice == 1}
    [{answer}]
        -> RightAnswer
+ {correctChoice != 1}
    [{wrongII}]
        -> WrongAnswer
+ {correctChoice == 2}
    [{answer}]
        -> RightAnswer
+ {correctChoice != 2}
    [{wrongIII}]
        -> WrongAnswer
+ {correctChoice == 3}
    [{answer}]
        -> RightAnswer
+ {correctChoice != 3}
    [{wrongIV}]
        -> WrongAnswer

=== RightAnswer ===
"{~Oh, so you are quiet good at math, I see|No way! You got it right!}"
~ MathScore++
-> MathTest

=== WrongAnswer ===
"WOMP WOMP"
"Sorry~ too bad"
"I think you are broke for a reason"
"But exercising is gonna help with that!"
"Maybe work on your brain a little bit, too!"
-> Leaving

=== Leaving ===
// Failure ending
"Enjoy your day!"
You left the gym. # narration
But it's hard when you realize you never canceled your gym membership. # narration
Over time, the forgotten fees quietly siphoned away your life savings. # narration
Now, you are homeless. # narration
Lying on the street, cold and hungry... # narration
How miserable. # narration
All you've done is just failed to cancel a membership. # narration
But that's a failure. # narration
Failures are unacceptable. # narration
And thus, you have failed your life. # narration # fail
-> END

=== Success ===
Congratulation! # narration
You have successfully cancelled your gym membership. # narration
No more random charges every months. # narration
Life seems a lot better now. # narration
The air feels fresher. # narration
The sun seems brighter. # narration
But all you want to do now, # narration
Is to lie on your bed, # narration
And doom scrolling on Instagram and TikTok. # narration
Eating junk foods, # narration
Playing Video games, # narration
Watching YouTube. # narration
Maybe, it's time for you to go out and touch grass. # narration # win
-> END

