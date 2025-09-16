(function () {
    const stage = document.getElementById('cardsStage');
    if (!stage) return;

    const cards = Array.from(stage.querySelectorAll('.card-item'));
    const btnPrev = document.getElementById('prevCard');
    const btnNext = document.getElementById('nextCard');

    let activeIndex = 0;

    function updateState() {
        cards.forEach((c, i) => {
            c.classList.remove('active', 'left', 'right', 'blurred');
            const cvv = c.querySelector('.cvv');
            if (cvv) {
                cvv.textContent = '***';
                cvv.classList.remove('revealed');
                cvv.classList.add('masked');
            }
            const btn = c.querySelector('.toggleReveal img');
            if (btn) btn.src = "/images/resources/hide.png";
        });

        cards.forEach((c, i) => {
            if (i < activeIndex) c.classList.add('left');
            if (i > activeIndex) c.classList.add('right');
        });

        const active = cards[activeIndex];
        active.classList.add('active');

        cards.forEach((c, i) => {
            if (i !== activeIndex) c.classList.add('blurred');
        });

        btnPrev.disabled = activeIndex === 0;
        btnNext.disabled = activeIndex === cards.length - 1;
    }

    if (btnPrev) btnPrev.addEventListener('click', () => { if (activeIndex > 0) { activeIndex--; updateState(); } });
    if (btnNext) btnNext.addEventListener('click', () => { if (activeIndex < cards.length - 1) { activeIndex++; updateState(); } });

    cards.forEach((card) => {
        const toggleBtn = card.querySelector('.toggleReveal');
        if (toggleBtn) {
            toggleBtn.addEventListener('click', () => {
                const cvv = card.querySelector('.cvv');
                const img = toggleBtn.querySelector('img');
                const isRevealed = cvv.classList.contains('revealed');

                if (isRevealed) {
                    cvv.textContent = '***';
                    cvv.classList.remove('revealed');
                    cvv.classList.add('masked');
                    img.src = "/images/resources/hide.png";
                } else {
                    cvv.textContent = card.dataset.cvv || '***';
                    cvv.classList.remove('masked');
                    cvv.classList.add('revealed');
                    img.src = "/images/resources/view.png";
                }
            });
        }
    });

    updateState();
})();

function showToast(message, type = "success") {
    const container = document.getElementById("toastContainer");
    if (!container) return;

    const toast = document.createElement("div");
    toast.className = `toast ${type}`;
    toast.textContent = message;

    container.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 4000);
}

function toastSuccess(msg) { showToast(msg, "success"); }
function toastError(msg) { showToast(msg, "error"); }


// Copy 
document.addEventListener('click', function (e) {
    const btn = e.target.closest && e.target.closest('.card-number');
    if (!btn) return;

    const full = btn.getAttribute('data-full');
    if (!full) return;

    if (navigator.clipboard && navigator.clipboard.writeText) {
        navigator.clipboard.writeText(full).then(() => {
            toastSuccess("Card number copied to clipboard!");
        }).catch(() => {
            toastError("Error occured while copying card number!");
        });
    } else {
        toastError("Clipboard API not supported");
    }
});

