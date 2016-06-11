(function () {
    timeClock.service('statusMessageService', function () {

        return {
            messages: [],

            get: function (clear) {
                var returnMessages = this.messages.slice(0);

                if (clear) {
                    this.messages = [];
                }
                return returnMessages;
            },

            add: function (message, level) {
                this.messages.push({ messageText: message, severityLevel: level});
            },

            remove: function(message) {
                var index = this.messages.indexOf(message);
                if (index > -1) {
                    this.messages.splice(index, 1);
                }
            }
        }
    });
})();

