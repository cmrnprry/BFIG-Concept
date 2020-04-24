#Scene_One
#location: cradle
A semi-abandoned hospital.

#Speaker: Nical
Huh. Looks like the cradle isn’t as busted as I thought. Give me a minute.

#Speaker: None
The player character (DIFF) gets its body.

#Speaker: Nical
Sorry about the wait. Getting you online now.

#Speaker: Nical
Welcome to existence. You’ll get used to it.->opening_choice

=== opening_choice ===
*Who are you? #Speaker: Diff
#Speaker: Nical
I’m an AI, like you and the others here on Natality Island. ->opening_choice
*Where is this?  #Speaker: Diff
#Speaker: Nical
Natality Island. It’s a hole, which is why they grow us AIs here. ->opening_choice
*What is happening?  #Speaker: Diff
#Speaker: Nical
Oof, that’s a lot. You, me, and the others here on Natality Island are AIs. ->opening_choice

//Fall Back
*->after_opeing_choice

=== after_opeing_choice ===
#Speaker: Nical
I’m Nical, by the way.

#Speaker: Nical
The basic idea is this is a place where they grow AIs, and they make us hang around and talk to each other until we answer that dumb question.

#Speaker: Diff
What is the difference engine?

#Speaker: Nical
That’s the one.

You sound a little flat, there. Maybe the damage to the cradle messed up your ability to process tone. ->second_opening_choice

=== second_opening_choice ===
#Speaker: Diff
*The cradle?
#Speaker: Nical
Yeah, this piece of junk. It’s how new AIs are made. It got banged up the other night, so that probably affected some subroutines in your software. ->second_opening_choice
#Speaker: Diff
*Tone?
#Speaker: Nical
You know, how you convey emotion when you communicate. The cradle must have gotten beat up while it was giving your software those subroutines.->second_opening_choice
#Speaker: Diff
*What do I do now?
#Speaker: Nical
I don’t really care, and there’s not much you can do with that chassis.->second_opening_choice
* -> after_second_opening_choice



=== after_second_opening_choice ===

#Speaker: Nical
Tell you what. Go bother the others. They might be able to help you figure out your tone problem.

#Speaker: Nical
While you’re out there, keep on the lookout for anything I can use to fix the cradle. If you can find a precision screwdriver, a soldering iron, and diagnosis software...

...Well, ask somebody with hands to bring them to me.
->DONE
