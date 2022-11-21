namespace TRLNFL.Slack
{
    public static class SlackClientTest
    {
        public static void TestPostMessage()
        {
            SlackClient client = new SlackClient();
            client.PostMessage("THIS IS A TEST MESSAGE! SQUEEDLYBAMBLYFEEDLYMEEDLYMOWWWWWWWW!");
        }
    }
}
