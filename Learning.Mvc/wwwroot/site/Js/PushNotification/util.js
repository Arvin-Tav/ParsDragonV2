
$(function () {
    checkRegisterServiceWorker();

    checkNotificationPromise();
});
function checkNotificationPromise() {
    if (Notification.permission !== "granted") {
        try {
            Notification.requestPermission().then();
        } catch (e) {
            return false;
        }
    }

    return true;
}

function registerServiceWorker() {
    return navigator.serviceWorker.register('/service-worker.js');
}

function checkRegisterServiceWorker() {
    return navigator.serviceWorker.getRegistration()
        .then(function (registration) {
            if (!registration) {
                return registerServiceWorker().then(function (registration) {
                });
            }
            return registerPush();
        });
}

function resetServiceWorkerAndPush() {
    return navigator.serviceWorker.getRegistration()
        .then(function (registration) {
            if (registration) {
                return registration.unregister();
            }
        })
        .then(function () {
            return registerServiceWorker().then(function (registration) {
                return registerPush();
            });
        });
}

function subscribePushAndUpdateButtons(registration) {
    return subscribePush(registration).then(function (subscription) {
        return subscription;
    });
}

function registerPush() {
    return navigator.serviceWorker.ready
        .then(function (registration) {
            return registration.pushManager.getSubscription().then(function (subscription) {
                if (subscription) {
                    // renew subscription if we're within 5 days of expiration
                    if (subscription.expirationTime && Date.now() > subscription.expirationTime - 432000000) {
                        return unsubscribePush().then(function () {
                            updateUnsubscribeButtons();
                            return subscribePushAndUpdateButtons(registration);
                        });
                    }

                    return subscription;
                }

                return subscribePushAndUpdateButtons(registration);
            });
        })
        .then(function (subscription) {
            return saveSubscription(subscription);
        });
}

function sendMessage(title, message, delay) {
    const notification = {
        title: title,
        body: message,
    };

    const userId = localStorage.getItem('userId');
    let apiUrl = `/Notification/send/${userId}`;
    if (delay) apiUrl += `?delay=${delay}`;

    return fetch(apiUrl, {
        method: 'post',
        headers: {
            'Content-type': 'application/json'
        },
        body: JSON.stringify(notification)
    });
}

function getPushSubscription() {
    return navigator.serviceWorker.ready
        .then(function (registration) {
            return registration.pushManager.getSubscription();
        });
}

function unsubscribePush() {
    return getPushSubscription().then(function (subscription) {
        return subscription.unsubscribe().then(function () {
            deleteSubscription(subscription);
        });
    });
}




function updateUnsubscribeButtons() {

}
