// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
	[Tooltip("Get the value of a Bool Variable from another FSM.")]
	public class GetFsmBool : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
        [UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		
        [RequiredField]
		[UIHint(UIHint.FsmBool)]
		public FsmString variableName;
		
        [RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool storeValue;
		
        public bool everyFrame;

	    private GameObject goLastFrame;
        private string fsmNameLastFrame;
	    private PlayMakerFSM fsm;
		
		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			storeValue = null;
		}

		public override void OnEnter()
		{
			DoGetFsmBool();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}

		public override void OnUpdate()
		{
			DoGetFsmBool();
		}

	    private void DoGetFsmBool()
		{
			if (storeValue == null) return;

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
            if (go != goLastFrame || fsmName.Value != fsmNameLastFrame)
            {
                goLastFrame = go;
                fsmNameLastFrame = fsmName.Value;
                // only get the fsm component if go or fsm name has changed
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}			
			
			if (fsm == null) return;
			
			var fsmBool = fsm.FsmVariables.GetFsmBool(variableName.Value);
			if (fsmBool == null) return;
			
			storeValue.Value = fsmBool.Value;
		}

	}
}