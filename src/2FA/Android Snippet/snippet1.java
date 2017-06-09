@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.tfa_settings, container, false);

//        cb_tfaenabled = (CheckBox) rootView.findViewById(R.id.cb_loggedin);
//        cb_deviceauthed = (CheckBox) rootView.findViewById(R.id.cb_welcomepage);

        cb_tfaenabled = (SwitchButton) rootView.findViewById(R.id.cb_tfaenabled);
        cb_deviceauthed = (SwitchButton) rootView.findViewById(R.id.cb_deviceauthed);

        //Fix these
        text_login = (TextView) rootView.findViewById(R.id.text_loggedin);
        text_welcome = (TextView) rootView.findViewById(R.id.text_welcomepage);


        btn_save = (Button) rootView.findViewById(R.id.btn_profile_save);
        btn_back = (Button) rootView.findViewById(R.id.btn_back);
        text_header = (TextView) rootView.findViewById(R.id.text_header);

        btn_save.setText(language.get("button.save"));
        btn_back.setText(language.get("button.back"));


        if(languageID.equals("1")){
            text_header.setTextSize(TypedValue.COMPLEX_UNIT_SP, 15);
        }
        //Fix this
        text_header.setText(language.get("settings.security.title"));

        //text_login.setText(language.get("settings.security.login"));
        //text_welcome.setText(language.get("settings.security.welcome"));

        boolean login = session.pref.getBoolean("autoLogin_"+username,true);
        boolean welcome = session.pref.getBoolean("welcome",true);

        Session s = new Session(context);
        NativeService service = new NativeService(context);
        String token = s.pref.getString("token", "");


        return rootView;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        context = getActivity();

        session = new Session(context);
        username = session.pref.getString("username", "");
        languageID = session.pref.getString("languageID", "2");
        language = new Language(languageID);
        
        
    }
    @Override
    public void onStart(){ // onCreate -> onCreateView -> onStart
        // PUT LOADING INDICATOR
        // DISABLE SWITCH BUTTONS

        new Thread(new Runnable() {
            @Override
            public void run() {
                // WEB SERVICE CALL
                UserGet2FAStatusResult TFAstatus = service.UserGet2FAStatus(token);
                activity.runOnUiThread(new Runnable() {
                    @Override
                    public void run() { //Runs on completion of 
                        // DO STUFF WHEN DATA IS LOADED
                        // ENABLE SWITCH BUTTONS
                        System.out.println(TFAstatus.isUser2FAEnabled());
                        System.out.println(TFAstatus.isSponsor2FAEnabled());

                        cb_tfaenabled.setChecked(TFAstatus.isUser2FAEnabled());
                        cb_deviceauthed.setChecked(TFAstatus.isSponsor2FAEnabled());
                        
                    }
                });

            }
        }).start();
    }