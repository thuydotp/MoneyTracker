<script>
export default {
  name: "modal",
  methods: {
    close() {
      this.$emit("close");
    }
  }
};
</script>

<template>
    <div>        
        <!-- MODAL -->    
        <div class="modal fade in show" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" @click="$emit('close')">
                            <span aria-hidden="true">&times;</span>
                        </button>            
                    </div>
                    <div class="modal-body">
                        <slot name="modal-body-slot"></slot>
                    </div>
                    <div class="modal-footer">
                        <slot name="modal-footer-slot"></slot>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal-backdrop fade in"></div>
    </div>   
</template>

<style>
.modal {
  display: block;
}

.modal-backdrop.in {
    filter: alpha(opacity=50);
    opacity: .5;
}
</style>