# PubNubMessageCounter
PubNub does not have straight forward way of calculating number of messages in a channel. This code can help you to do that easily.

Getting history from Storage will just give you the messages themselves. The max number of messages you can retrieve per history call is 100. To get all the messages from a channel, you have to call history recursively using the start timetoken of the response to page through storage until you get less than 100 messages.
Here’s a KB article that outlines this: https://www.pubnub.com/knowledge-base/discussion/194/how-do-i-page-through-stored-messages
And the history call for C# example is here: https://www.pubnub.com/docs/c-sharp-dot-net-c-sharp/storage-and-history

**P.S. Remember that every message retrieved by history is counted towards the messages per month for the account.**
