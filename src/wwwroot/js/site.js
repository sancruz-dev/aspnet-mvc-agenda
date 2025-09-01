document.addEventListener('DOMContentLoaded', function () {
    const confirmModal = new bootstrap.Modal(document.getElementById('confirmModal'));
    const toast = new bootstrap.Toast(document.getElementById('toast'));

    function showToast(message, type = 'info') {
        const toastBody = document.getElementById('toastBody');
        const toastHeader = document.querySelector('#toast .toast-header i');

        const types = {
            success: { icon: 'bi-check-circle', class: 'text-success' },
            error: { icon: 'bi-x-circle', class: 'text-danger' },
            warning: { icon: 'bi-exclamation-triangle', class: 'text-warning' },
            info: { icon: 'bi-info-circle', class: 'text-primary' }
        };

        const config = types[type] || types.info;
        toastHeader.className = `${config.icon} me-2 ${config.class}`;
        toastBody.textContent = message;

        toaster.show();
    }

    window.confirmDeleteContato = function (id, nome) {
        document.getElementById('deleteId').value = id;
        document.getElementById('confirmModalBody').innerHTML =
            `Tem certeza que deseja excluir o contato <strong>"${nome}"</strong>?<br><small class="text-muted">Esta ação não pode ser desfeita.</small>`;
        confirmModal.show();
    };
});